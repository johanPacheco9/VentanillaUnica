using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Models;

// Models/GestionSolicitud.cs
public class GestionSolicitud : EntityWithTraceability
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public Solicitud Solicitud { get; set; } = null!;

    public EstadoSolicitud EstadoAnterior { get; set; }
    public EstadoSolicitud EstadoNuevo    { get; set; }

    public int? FuncionarioAnteriorId { get; set; }
    public int? FuncionarioNuevoId    { get; set; }
    public Funcionario? FuncionarioNuevo { get; set; }

    public string? Observacion { get; set; }
    public DateTime FechaGestion { get; set; } = DateTime.UtcNow;

    public string? RealizadoPor { get; set; }
}