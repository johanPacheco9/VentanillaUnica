using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Models;

public sealed class Solicitud : EntityWithTraceability
{
    public int Id { get; set; }
    public string NumeroRadicado { get; set; } = string.Empty;

    public int CiudadanoId { get; set; }
    public Ciudadano Ciudadano { get; set; } = null!;

    public int TramiteId { get; set; }
    
    public Tramite Tramite { get; set; } = null!;
    
    public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;
    
    //Fecha cuando se crea la solicitud o radicado.
    public DateTime FechaSolicitud { get; set; }
    
    //Fecha cuando se asigna por primera vez al funcionario.
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    
    //Si se necesita, seria pedirla
    public DateTime? FechaEstimadaFin { get; set; }
    
    //Al cambiar al estado completada.
    public DateTime? FechaFinReal { get; set; }

    public string? Observaciones { get; set; }
    
    public Origen? Origen { get; set; }

    public string? Placa { get; set; }
    //Relaciones
    public int? FuncionarioId { get; set; }
    public Funcionario? Funcionario { get; set; }
    
    public ICollection<GestionSolicitud> Gestiones { get; set; } = new List<GestionSolicitud>();
}