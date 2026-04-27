using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Gestion.Requests;

public class CreateGestionRequest
{
    public int SolicitudId { get; set; }
    public int? FuncionarioId { get; set; }
    public EstadoSolicitud NuevoEstado { get; set; }
    public int? NuevoFuncionarioAsignado { get; set; }
    public string? Observacion { get; set; }
    public string? RealizadoPor { get; set; }
}
