using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager
{
    public async Task<Models.Solicitud> UpdateAsync(UpdateSolicitudRequest request)
    {
        var solicitud = await _dbContext.Solicitudes
                            .FirstOrDefaultAsync(s => s.Id == request.SolicitudId)
                        ?? throw new KeyNotFoundException("Solicitud no encontrada.");

        var gestion = new GestionSolicitud
        {
            SolicitudId           = solicitud.Id,
            EstadoAnterior        = solicitud.Estado,
            EstadoNuevo           = request.EstadoNuevo,
            FuncionarioAnteriorId = solicitud.FuncionarioId,
            FuncionarioNuevoId    = request.FuncionarioNuevoId,
            Observacion           = request.Observacion,
            FechaGestion          = DateTime.UtcNow
        };

        solicitud.Estado        = request.EstadoNuevo;
        solicitud.FuncionarioId = request.FuncionarioNuevoId;

        if (request.EstadoNuevo == EstadoSolicitud.Completada)
            solicitud.FechaFinReal = DateTime.UtcNow;

        _dbContext.GestionesSolicitud.Add(gestion);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Solicitud {Id} actualizada: {EstadoAnterior} → {EstadoNuevo}",
            solicitud.Id, request.EstadoAnterior, request.EstadoNuevo);

        return solicitud;
    }

    public async Task<List<GestionSolicitud>> GetGestionesAsync(int solicitudId)
    {
        return await _dbContext.GestionesSolicitud
            .AsNoTracking()
            .Include(g => g.FuncionarioNuevo)
            .Where(g => g.SolicitudId == solicitudId)
            .OrderByDescending(g => g.FechaGestion)
            .ToListAsync();
    }
}