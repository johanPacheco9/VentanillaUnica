using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud.Requests;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager
{
    public async Task<Models.Solicitud> CrearAsync(CreateSolicitudRequest request)
    {
        var tramite = await _dbContext.Tramites
                          .Include(t => t.TipoTramite)
                          .FirstOrDefaultAsync(t => t.Id == request.TipoTramiteId)
                      ?? throw new KeyNotFoundException("Tipo de trámite no encontrado.");
    
        if (tramite.TipoTramite?.RequierePlaca == true)
        {
            if (string.IsNullOrWhiteSpace(request.Placa))
            {
                _logger.LogWarning("Se intentó crear una solicitud sin placa.");
                throw new Exception("La placa es obligatoria para este tipo de trámite.");
            }
        }

        var solicitud = new Models.Solicitud
        {
            NumeroRadicado   = await GenerarRadicadoAsync(),
            CiudadanoId      = request.CiudadanoId,
            TramiteId    = request.TipoTramiteId,
            Observaciones    = request.Observaciones,
            FechaInicio      = DateTime.UtcNow,
            FechaSolicitud   = DateTime.UtcNow,
            FechaEstimadaFin = SumarDiasHabiles(DateTime.UtcNow, tramite.DiasEstimados ?? 0),
            Origen = request.Origen,
            Placa = request.Placa,
            CreadoPor = "System",
            FechaCreacion =  DateTime.UtcNow
        };

        _dbContext.Solicitudes.Add(solicitud);
        await _dbContext.SaveChangesAsync();
        return solicitud;
    }

    private async Task<string> GenerarRadicadoAsync()
    {
        var anio  = DateTime.UtcNow.Year;
        var count = await _dbContext.Solicitudes
            .CountAsync(s => s.FechaInicio.Year == anio);

        return $"RAD-{anio}-{(count + 1):D5}";
    }
    
    public static DateTime SumarDiasHabiles(DateTime fechaInicial, int diasASumar)
    {
        DateTime fechaFinal = fechaInicial;
        int diasContados = 0;

        while (diasContados < diasASumar)
        {
            fechaFinal = fechaFinal.AddDays(1);
            // Si no es sábado ni domingo, contamos el día
            if (fechaFinal.DayOfWeek != DayOfWeek.Saturday && fechaFinal.DayOfWeek != DayOfWeek.Sunday)
            {
                diasContados++;
            }
        }
        return fechaFinal;
    }
}