using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Gestion.Requests;
namespace VentanillaUnica.Services.Gestion;

public partial class GestionManager
{
    public async Task Create(CreateGestionRequest request)
    {
        // Cargar la solicitud actual
        var solicitud = await _dbContext.Solicitudes
            .FirstOrDefaultAsync(s => s.Id == request.SolicitudId);

        if (solicitud is null)
            throw new InvalidOperationException("Solicitud no encontrada");

        // Crear la gestión con los datos anteriores y nuevos
        var gestion = new GestionSolicitud
        {
            SolicitudId = solicitud.Id,
            EstadoAnterior = solicitud.Estado,
            EstadoNuevo = request.NuevoEstado,
            FuncionarioAnteriorId = solicitud.FuncionarioId,
            FuncionarioNuevoId = request.NuevoFuncionarioAsignado,
            Observacion = request.Observacion,
            FechaGestion = DateTime.UtcNow,
            RealizadoPor = request.RealizadoPor
        };

        solicitud.Estado = request.NuevoEstado;
        solicitud.FuncionarioId = request.NuevoFuncionarioAsignado;
        
        _dbContext.GestionesSolicitud.Add(gestion);
        await _dbContext.SaveChangesAsync();
    }
    
}