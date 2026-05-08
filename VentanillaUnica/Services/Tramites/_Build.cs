using VentanillaUnica.Data;
namespace VentanillaUnica.Services.Tramites;

public partial class TramitesManager(AppDbContext context, ILogger<TramitesManager> logger)
{
   private readonly AppDbContext _context = context;
   private readonly ILogger<TramitesManager> _logger = logger;
}