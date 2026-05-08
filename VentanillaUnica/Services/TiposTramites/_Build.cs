using Microsoft.AspNetCore.Components;
using VentanillaUnica.Data;
namespace VentanillaUnica.Services.TiposTramites;

public partial class TiposTramitesManager(AppDbContext context, ILogger<TiposTramitesManager> logger)
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<TiposTramitesManager> _logger = logger;
}