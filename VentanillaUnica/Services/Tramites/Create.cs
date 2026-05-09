using VentanillaUnica.Models;
using VentanillaUnica.Services.Tramites.Requests;
namespace VentanillaUnica.Services.Tramites;

public partial class TramitesManager
{
    public async Task<Tramite?> CreateTramite(CreateTramiteRequest request)
    {
        try
        {
            var tramite = new Tramite
            {
                Nombre =  request.Nombre,
                Descripcion = request.Descripcion,
                TipoTramiteId =  request.TipoTramiteId,
                DiasEstimados = request.DiasEstimados,
                CreadoPor = request.CreadoPor,
                FechaCreacion = DateTime.UtcNow,
            };
            _context.Tramites.Add(tramite);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Trámite {Nombre} creado con éxito.", tramite.Nombre);
            return tramite;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el trámite: {Message}", e.Message);
            return null;
        }
    }
}