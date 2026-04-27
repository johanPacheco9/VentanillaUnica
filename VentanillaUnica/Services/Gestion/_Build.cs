using VentanillaUnica.Data;
namespace VentanillaUnica.Services.Gestion;

public partial class GestionManager(ILogger<GestionManager> logger, IConfiguration configuration, AppDbContext dbContext)
{
    private readonly ILogger<GestionManager> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly AppDbContext _dbContext = dbContext;
}
