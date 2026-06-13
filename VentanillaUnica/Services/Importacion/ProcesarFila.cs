using System.Text.RegularExpressions;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Importacion.Requests;
using VentanillaUnica.Models;
namespace VentanillaUnica.Services.Importacion;

public partial class ImportacionService
{
    public List<(Models.Ciudadano Ciudadano, SolicitudRequestDto Solicitud, bool TieneError, string Mensaje)> ProcesarFilaExcel(
        string datosNombre,
        string numDocumento,
        string fechaRaw,
        string radicadoRaw,
        string foliosRaw,
        string placaRaw,
        string asuntoRaw,
        string responsableRaw)
    {
    
        var resultados = new List<(Models.Ciudadano, SolicitudRequestDto, bool, string)>();

        string[] nombresSeparados = datosNombre.Split(new[] { " - ", " – " }, StringSplitOptions.RemoveEmptyEntries);
        string[] docsSeparados = numDocumento.Split(new[] { " - ", " – ", "-" }, StringSplitOptions.RemoveEmptyEntries);

        int totalCiudadanos = Math.Max(1, nombresSeparados.Length);

        for (int i = 0; i < totalCiudadanos; i++)
        {
            var errores = new List<string>();
            var ciudadano = new VentanillaUnica.Models.Ciudadano();
            var solicitud = new SolicitudRequestDto();

            string nombreFila = nombresSeparados.Length > i ? nombresSeparados[i] : datosNombre;
            string docFila = docsSeparados.Length > i ? docsSeparados[i] : numDocumento;

            // --- A. PROCESAR DOCUMENTO Y TIPO ---
            string docNormalizado = NormalizarTexto(docFila);

            if (docNormalizado.Contains("NIT"))
            {
                ciudadano.TipoDocumento = TipoDocumento.NIT;
                string nitConGuion = Regex.Replace(docNormalizado, @"[^0-9\-]", "").Trim('-');
                ciudadano.NumeroDocumento = Truncar(nitConGuion, 20);
            }
            else if (docNormalizado.Contains("PASAPORTE") || docNormalizado.Contains("PAS"))
            {
                ciudadano.TipoDocumento = TipoDocumento.PAS;
                ciudadano.NumeroDocumento = Truncar(Regex.Replace(docNormalizado, @"[^A-Z0-9]", ""), 20);
            }
            else if (string.IsNullOrWhiteSpace(docNormalizado) || (Regex.IsMatch(docNormalizado, @"[A-Z]") && docNormalizado.Length > 10))
            {
                // Si no hay documento o es un texto administrativo largo (como secretarías sin NIT explícito en la celda)
                ciudadano.TipoDocumento = TipoDocumento.NIT;
                ciudadano.NumeroDocumento = "899999111"; // Un NIT genérico institucional para que no rompa la BD
            }
            else
            {
                ciudadano.TipoDocumento = TipoDocumento.CC;
                ciudadano.NumeroDocumento = Truncar(Regex.Replace(docNormalizado, @"[^0-9]", ""), 20);
            }

            // --- B. PROCESAR NOMBRE (Blindaje Institucional) ---
            string nombreCompleto = NormalizarTexto(nombreFila);
            if (string.IsNullOrEmpty(nombreCompleto))
            {
                errores.Add("El nombre del ciudadano está vacío.");
            }
            // 🏢 Si el solicitante es una entidad pública o secretaría, evitamos romper el nombre en pedazos
            else if (nombreCompleto.Contains("SECRETARIA") || 
                     nombreCompleto.Contains("HACIENDA")   || 
                     nombreCompleto.Contains("ALCALDIA")    ||
                     nombreCompleto.Contains("VEREDA")      ||
                     nombreCompleto.Contains("CONJUNTO")    ||
                     nombreCompleto.Contains("EMPRESA")     ||
                     nombreCompleto.Contains("ASOCIACION")  ||
                     nombreCompleto.Contains("COOPERATIVA") ||
                     nombreCompleto.Contains("FUNDACION"))
            {
                ciudadano.PrimerNombre   = Truncar(nombreCompleto, 50);
                ciudadano.SegundoNombre  = string.Empty;
                ciudadano.PrimerApellido = "ENTIDAD";
                ciudadano.SegundoApellido = "PUBLICA";
            }
            else
            {
                MapearNombreCompleto(nombreCompleto, ciudadano);
            }

            ciudadano.FechaRegistro = DateTime.UtcNow;

            // --- C. PROCESAR FECHA, RADICADO Y FOLIOS ---
            // --- C. PROCESAR FECHA, RADICADO Y FOLIOS ---
            if (DateTime.TryParse(fechaRaw?.Trim(), out DateTime fechaConvertida))
            {
                solicitud.Fecha = DateTime.SpecifyKind(fechaConvertida, DateTimeKind.Utc);
            }
            else
            {
                solicitud.Fecha = DateTime.UtcNow;
            }

            solicitud.NumeroFolio = Regex.Replace(foliosRaw ?? "", @"[^\d]", "");
            solicitud.NumeroRadicado = Regex.Replace(radicadoRaw ?? "", @"[^\d]", "");

            // 🎯 Forzamos a que las observaciones guarden el asunto real que viene de la columna exacta
            solicitud.Anotaciones = NormalizarTexto(asuntoRaw);

            // --- D. PROCESAR PLACAS MÚLTIPLES ---
            string poolPlacas = $"{NormalizarTexto(placaRaw)} {docNormalizado}";
            var matchesPlacas = Regex.Matches(poolPlacas, @"\b[A-Z]{3}[0-9]{2}[A-Z0-9]\b");

            foreach (Match match in matchesPlacas)
            {
                if (!solicitud.Placas.Contains(match.Value))
                    solicitud.Placas.Add(match.Value);
            }

            bool tieneError = errores.Any();
            string mensaje = tieneError ? string.Join(" | ", errores) : "OK";

            resultados.Add((ciudadano, solicitud, tieneError, mensaje));
        }

        return resultados;
    }

    private string Truncar(string texto, int maxLength)
    {
        if (string.IsNullOrEmpty(texto)) return string.Empty;

        return texto.Length <= maxLength ? texto : texto.Substring(0, maxLength).Trim();
    }
}