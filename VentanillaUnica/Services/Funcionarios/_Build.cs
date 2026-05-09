using VentanillaUnica.Data;
namespace VentanillaUnica.Services.Funcionarios;
public partial class FuncionarioManager(ILogger<FuncionarioManager> logger, IConfiguration configuration, AppDbContext dbContext)
{
    private readonly ILogger<FuncionarioManager> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly AppDbContext _dbContext = dbContext;
}
