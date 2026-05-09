using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.TiposTramites.Requests;

namespace VentanillaUnica.Services.TiposTramites;

public partial class TiposTramitesManager
{
    public async Task<bool> UpdateTipoTramite(UpdateTipoTramiteRequest request)
    {
        try
        {
            var tipoTramite = await _context.TipoTramites
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (tipoTramite is null) return false;

            tipoTramite.Name          = request.Name;
            tipoTramite.Description   = request.Description;
            tipoTramite.RequierePlaca = request.RequierePlaca;
            tipoTramite.FechaModificacion = DateTime.UtcNow;
            tipoTramite.ModificadoPor = request.ModificadoPor;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar Tipo de Trámite: {Message}", e.Message);
            return false;
        }
    }
}