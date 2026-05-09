using VentanillaUnica.Models;
using VentanillaUnica.Services.TiposTramites.Requests;
namespace VentanillaUnica.Services.TiposTramites;

public partial class TiposTramitesManager
{
    public async Task<TipoTramite?> CreateTipoTramite(CreateTipoTramiteRequest request)
    {
        try
        {
            var tipoTramite = new TipoTramite
            { 
                Name = request.Name,
                Description = request.Description,
                RequierePlaca = request.RequierePlaca,
                CreadoPor = request.CreadoPor,
                FechaCreacion =  DateTime.UtcNow
            };
            _context.TipoTramites.Add(tipoTramite);
            await _context.SaveChangesAsync();

            return tipoTramite;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear Tipo de Trámite: {Message}", e.Message);
            return null;
        }
        
    }
}