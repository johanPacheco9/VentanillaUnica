using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.Tramites.Requests;
using VentanillaUnica.Services.Tramites.Responses;
namespace VentanillaUnica.Services.Tramites;

public partial class TramitesManager
{
    public async Task<List<TramitesByTipoDetailDto>> GetByTipoTramites(GetTramitesByTipoRequest request)
    {
        var query = _context.Tramites.AsNoTracking().Where(t => t.Activo);

        // 1. Filtros
        if (request.TipoTramiteId.HasValue && request.TipoTramiteId > 0)
        {
            query = query.Where(t => t.TipoTramiteId == request.TipoTramiteId);
        }

        // 2. ORDENAR AQUÍ (Sobre la entidad original)
        // Usamos 'Name' porque es la propiedad de la clase Tramite
        query = query.OrderBy(t => t.Nombre);

        // 3. PROYECTAR AL DTO
        return await query
            .Select(t => new TramitesByTipoDetailDto(
                t.Id,
                t.Nombre, // Este se convierte en 'Nombre' en el DTO
                t.DiasEstimados,
                t.TipoTramite != null && (t.TipoTramite.RequierePlaca ?? false),
                t.TipoTramiteId 
            ))
            .ToListAsync();
    }
}