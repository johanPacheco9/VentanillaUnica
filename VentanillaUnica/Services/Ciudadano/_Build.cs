using VentanillaUnica.Data;
namespace VentanillaUnica.Services.Ciudadano;

public partial class CiudadanoManager(ILogger<CiudadanoManager> logger, IConfiguration configuration, AppDbContext dbContext)
{
    private readonly ILogger<CiudadanoManager> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly AppDbContext _dbContext = dbContext;
}
