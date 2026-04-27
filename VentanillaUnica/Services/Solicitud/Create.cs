using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud.Requests;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager
{
    public async Task<Models.Solicitud> CrearAsync(CreateSolicitudRequest request)
    {
        var tipo = await _dbContext.Tramites.FindAsync(request.TipoTramiteId)
                   ?? throw new KeyNotFoundException("Tipo de trámite no encontrado.");

        var solicitud = new Models.Solicitud
        {
            NumeroRadicado   = await GenerarRadicadoAsync(),
            CiudadanoId      = request.CiudadanoId,
            TipoTramiteId    = request.TipoTramiteId,
            Observaciones    = request.Observaciones,
            FechaInicio      = DateTime.UtcNow,
            FechaSolicitud   = DateTime.UtcNow,
            FechaEstimadaFin = DateTime.UtcNow.AddDays(tipo.DiasEstimados),
            Origen = request.Origen
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

        return $"VU-{anio}-{(count + 1):D5}";
    }
}