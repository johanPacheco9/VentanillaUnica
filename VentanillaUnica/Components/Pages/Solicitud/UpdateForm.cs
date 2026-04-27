using System.ComponentModel.DataAnnotations;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Components.Pages.Solicitud;

public class UpdateSolicitudForm
{
    [Required(ErrorMessage = "Debe seleccionar un funcionario.")]
    public int? FuncionarioId { get; set; }

    [Required(ErrorMessage = "El estado es requerido.")]
    public EstadoSolicitud Estado { get; set; }

    [MaxLength(500)]
    public string? Observacion { get; set; }
}