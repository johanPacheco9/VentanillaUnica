namespace VentanillaUnica.Components.Pages.Tramites;
using System.ComponentModel.DataAnnotations;

public class CreateTramiteForm
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    [MaxLength(150)]
    public string Nombre { get; set; } = null!;

    [MaxLength(500)]
    public string? Descripcion { get; set; }

    [Range(1, 365, ErrorMessage = "Los días estimados deben estar entre 1 y 365.")]
    public int? DiasEstimados { get; set; }

    public int? TipoTramiteId { get; set; }
}