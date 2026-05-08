using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Tramites.Requests;

namespace VentanillaUnica.Services.Tramites;

public partial class TramitesManager
{
    public async Task<List<Tramite>> GetTramites(ListTramitesRequest request)
    {
        var query = _context.Tramites
            .AsNoTracking()
            .Include(t => t.TipoTramite)
            .AsQueryable();

        if (request.Activo.HasValue)
            query = query.Where(t => t.Activo == request.Activo);

        if (request.TipoTramiteId.HasValue)
            query = query.Where(t => t.TipoTramiteId == request.TipoTramiteId);

        return await query
            .OrderBy(t => t.Nombre)
            .Skip((request.Pagina - 1) * request.TamañoPagina)
            .Take(request.TamañoPagina)
            .ToListAsync();
    }
}