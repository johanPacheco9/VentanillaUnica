using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Importacion.Requests;
using VentanillaUnica.Services.Solicitud.Enums;

namespace VentanillaUnica.Services.Importacion;

public partial class ImportacionService
{
    public async Task ImportarDesdeExcelAsync(List<RegistroExcelRaw> filasExcel)
    {
        _logger.LogInformation("🚀 Iniciando importación masiva de {Count} registros desde Excel...", filasExcel.Count);

        // 1. PRECARGAR DATOS A MEMORIA (Un solo viaje a la BD por tabla para máxima velocidad)
        var tramitesExistentes = await _context.Tramites.ToListAsync();
        var funcionariosExistentes = await _context.Funcionario.ToListAsync();
        
        // Caché de ciudadanos indexado para búsquedas instantáneas en memoria O(1)
        var ciudadanosCache = await _context.Ciudadanos
            .ToDictionaryAsync(c => $"{c.NumeroDocumento}_{c.TipoDocumento}");

        // 2. OPTIMIZACIÓN DEL RADICADO: Conteo base inicial una sola vez
        var anioActual = DateTime.UtcNow.Year;
        var contadorRadicadoBase = await _context.Solicitudes
            .CountAsync(s => s.FechaInicio.Year == anioActual);

        // Bucle principal sobre las filas del Excel
        foreach (var fila in filasExcel)
        {
            try
            {
                var registrosProcesados = ProcesarFilaExcel(
                    fila.Datos,                // 1
                    fila.NumeroIdentificacion, // 2
                    fila.Fecha,                // 3
                    fila.NoRadicado,           // 4
                    fila.NoFolios,             // 5
                    fila.Placa,                // 6
                    fila.Asunto,               // 7
                    fila.Responsable           // 8 
                );

                foreach (var (ciudadanoLimpio, solicitudLimpia, tieneError, mensaje) in registrosProcesados)
                {
                    if (tieneError && ciudadanoLimpio.NumeroDocumento == "0") continue;
                    
                    string llaveCache = $"{ciudadanoLimpio.NumeroDocumento}_{ciudadanoLimpio.TipoDocumento}_{ciudadanoLimpio.PrimerNombre}";
                    Models.Ciudadano ciudadanoAsignado;

                    if (ciudadanosCache.TryGetValue(llaveCache, out var ciudadanoExistente))
                    {
                        ciudadanoAsignado = ciudadanoExistente;
                    }
                    else
                    {
                        // 🚨 BLINDAJE ANTI-ERROR 22001 (Recorte estricto a 50 caracteres)
                        ciudadanoLimpio.PrimerNombre   = Truncar(ciudadanoLimpio.PrimerNombre, 50);
                        ciudadanoLimpio.SegundoNombre  = Truncar(ciudadanoLimpio.SegundoNombre ?? string.Empty, 50);
                        ciudadanoLimpio.PrimerApellido = Truncar(ciudadanoLimpio.PrimerApellido, 50);
                        ciudadanoLimpio.SegundoApellido = Truncar(ciudadanoLimpio.SegundoApellido ?? string.Empty, 50);

                        // Comodín por si el mapeo del Excel rompió o dejó vacío el campo obligatorio
                        if (string.IsNullOrWhiteSpace(ciudadanoLimpio.PrimerNombre)) ciudadanoLimpio.PrimerNombre = "SINDATO";
                        if (string.IsNullOrWhiteSpace(ciudadanoLimpio.PrimerApellido)) ciudadanoLimpio.PrimerApellido = "SINDATO";
                        
                        ciudadanoLimpio.NumeroDocumento = Truncar(ciudadanoLimpio.NumeroDocumento, 20);

                        // Agregamos de forma segura al contexto y al caché de memoria
                        _context.Ciudadanos.Add(ciudadanoLimpio);
                        ciudadanosCache.Add(llaveCache, ciudadanoLimpio);
                        ciudadanoAsignado = ciudadanoLimpio;
                    }

                    // --- CONTROL DE TRÁMITES DINÁMICOS SEGÚN EL ASUNTO ---
                    // 🎯 Usamos el asunto limpio proveniente de la lectura de la columna para evitar arrastres
                    string nombreTramite = solicitudLimpia.Anotaciones; 
                    if (string.IsNullOrWhiteSpace(nombreTramite)) nombreTramite = "TRAMITE GENERAL / MIGRACION";

                    var tramiteAsignado = tramitesExistentes
                        .FirstOrDefault(t => t.Nombre.Equals(nombreTramite, StringComparison.OrdinalIgnoreCase));

                    if (tramiteAsignado == null)
                    {
                        tramiteAsignado = new Models.Tramite
                        {
                            Nombre = Truncar(nombreTramite, 100), 
                            Descripcion = $"Trámite creado automáticamente desde el historial de Excel",
                            Activo = true,
                            DiasEstimados = 15
                        };
                        _context.Tramites.Add(tramiteAsignado);
                        tramitesExistentes.Add(tramiteAsignado); 
                    }

                    // --- CONTROL DE FUNCIONARIOS Y ESTADO DINÁMICO ---
                    Models.Funcionario? funcionarioAsignado = null;
                    EstadoSolicitud estadoCalculado = EstadoSolicitud.Pendiente;
                    string responsableNormalizado = NormalizarTexto(fila.Responsable ?? string.Empty);

                    // 🎯 Determinamos si viene o no un responsable real en la celda del Excel
                    if (!string.IsNullOrWhiteSpace(responsableNormalizado))
                    {
                        string rawResponsable = NormalizarTexto(responsableNormalizado);
                        string[] partesFuncionario = rawResponsable.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        string fPrimerNombre = partesFuncionario.Length > 0 ? partesFuncionario[0] : "SISTEMA";
                        string fPrimerApellido = partesFuncionario.Length > 1 ? partesFuncionario[1] : "MIGRADO";

                        if (fPrimerApellido == "Y" && partesFuncionario.Length > 2) fPrimerApellido = partesFuncionario[2];

                        fPrimerNombre = Truncar(fPrimerNombre, 50);
                        fPrimerApellido = Truncar(fPrimerApellido, 50);

                        funcionarioAsignado = funcionariosExistentes.FirstOrDefault(f =>
                            (f.PrimerNombre.Equals(fPrimerNombre, StringComparison.OrdinalIgnoreCase) &&
                             f.PrimerApellido.Equals(fPrimerApellido, StringComparison.OrdinalIgnoreCase)) ||
                            f.NombreCompleto.Contains(rawResponsable, StringComparison.OrdinalIgnoreCase) ||
                            (rawResponsable.Contains(f.PrimerNombre, StringComparison.OrdinalIgnoreCase) && partesFuncionario.Length == 1)
                        );

                        if (funcionarioAsignado == null)
                        {
                            funcionarioAsignado = new Models.Funcionario
                            {
                                PrimerNombre = fPrimerNombre,
                                PrimerApellido = fPrimerApellido,
                                Activo = true,
                                Email = $"{Truncar(fPrimerNombre.ToLower(), 30)}@ventanillaunica.gov.co"
                            };
                            _context.Funcionario.Add(funcionarioAsignado);
                            funcionariosExistentes.Add(funcionarioAsignado);
                        } 

                        // Si encontramos o creamos un funcionario válido, el estado cambia a Asignada
                        estadoCalculado = EstadoSolicitud.Asignada;
                    }
                    // NOTA: Si el bloque 'else' se ejecuta (fila.Responsable vacío), 
                    // 'funcionarioAsignado' permanece como null y el estado se queda en EstadoSolicitud.Pendiente.

                    // Usar el radicado del Excel, con fallback si viene vacío
                    string nuevoRadicado = !string.IsNullOrWhiteSpace(solicitudLimpia.NumeroRadicado)
                        ? solicitudLimpia.NumeroRadicado
                        : $"RAD-{anioActual}-{++contadorRadicadoBase:D5}";

                    // --- CREACIÓN DE LA SOLICITUD ASOCIADA ---
                    var nuevaSolicitud = new Models.Solicitud
                    {
                        Ciudadano      = ciudadanoAsignado,
                        Tramite        = tramiteAsignado,
                        Funcionario    = funcionarioAsignado, // Recibe la entidad o null directamente
                        Estado         = estadoCalculado,     // Asignada o Pendiente según corresponda
                        FechaSolicitud = solicitudLimpia.Fecha,    
                        FechaInicio    = solicitudLimpia.Fecha,
                        NumeroRadicado = nuevoRadicado,
                        NumeroFolio    = solicitudLimpia.NumeroFolio,
                        Observaciones  = solicitudLimpia.Anotaciones,
                        Origen         = Origen.Ventanilla,
                        CreadoPor      = "Importación Excel",
                        Placas         = solicitudLimpia.Placas
                            .Where(p => !string.IsNullOrWhiteSpace(p))
                            .Select(p => new Models.SolicitudPlaca { Placa = Truncar(p, 10) })
                            .ToList()
                    };

                    _context.Solicitudes.Add(nuevaSolicitud);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crítico procesando en memoria la fila con datos: {Datos}", fila.Datos);
            }
        }

        // 3. UN ÚNICO GUARDADO FINAL EN BLOQUE (Batching eficiente de Npgsql)
        try
        {
            _logger.LogInformation("💾 Descargando lote completo de inserciones en PostgreSQL...");
            await _context.SaveChangesAsync();
            _logger.LogInformation("✅ ¡Importación masiva finalizada con éxito!");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "🚨 Error fatal al persistir el lote de la importación en la base de datos.");
            _context.ChangeTracker.Clear(); 
            throw;
        }
    }
}