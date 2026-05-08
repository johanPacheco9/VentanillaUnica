using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Tramites.Requests;

namespace VentanillaUnica.Services.Tramites;

public partial class TramitesManager
{
    public async Task<Tramite?> UpdateTramite(UpdateTramiteRequest request)
    {
        try
        {
            var tramite = await _context.Tramites
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (tramite == null)
            {
                _logger.LogWarning("Se intentó actualizar el trámite {Id} pero no existe.", request.Id);
                return null;
            }

            tramite.Nombre = request.Nombre;
            tramite.Descripcion = request.Descripcion;
            tramite.DiasEstimados = request.DiasEstimados;
            tramite.TipoTramiteId = request.TipoTramiteId;
            tramite.Activo = request.Activo;
            tramite.FechaModificacion = DateTime.UtcNow;
            tramite.ModificadoPor = "system";
            
            await _context.SaveChangesAsync();

            _logger.LogInformation("Trámite {Id} actualizado correctamente.", tramite.Id);
            return tramite;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el trámite {Id}: {Message}", request.Id, e.Message);
            throw;
        }
    }

    public async Task<Tramite?> GetByIdAsync(int id)
    {
        return await _context.Tramites
            .Include(t => t.TipoTramite)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}