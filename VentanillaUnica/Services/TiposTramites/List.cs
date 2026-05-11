using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.TiposTramites.Responses;
namespace VentanillaUnica.Services.TiposTramites;

public partial class TiposTramitesManager
{
    public async Task<List<TiposTramiteResponseDto>> List()
    {
        return await _context.TipoTramites
            .Select(t => new TiposTramiteResponseDto(t.Id, t.Name,t.RequierePlaca ?? false))
            .ToListAsync();
    }
}
