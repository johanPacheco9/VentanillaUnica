using VentanillaUnica.Data;
using VentanillaUnica.Services.Solicitud;
namespace VentanillaUnica.Services.Importacion;

public partial class ImportacionService(AppDbContext context, ILogger<ImportacionService> logger, SolicitudManager solicitudManager)
{
    private readonly  AppDbContext _context = context;
    private readonly  ILogger<ImportacionService> _logger = logger;
    private readonly SolicitudManager _solicitudManager = solicitudManager;
}