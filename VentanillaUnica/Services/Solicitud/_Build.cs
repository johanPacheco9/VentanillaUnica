using VentanillaUnica.Data;
using VentanillaUnica.Services.Solicitud;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager(ILogger<SolicitudManager> logger, IConfiguration configuration, AppDbContext dbContext)
{
    private readonly ILogger<SolicitudManager> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly AppDbContext _dbContext = dbContext;
}