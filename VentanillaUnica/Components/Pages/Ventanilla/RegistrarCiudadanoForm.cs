using System.ComponentModel.DataAnnotations;

namespace VentanillaUnica.Components.Pages.Ventanilla;

public class RegistrarCiudadanoForm
{
    [Required(ErrorMessage = "El primer nombre es requerido.")]
    [MaxLength(100)]
    public string PrimerNombre { get; set; } = null!;

    [MaxLength(100)]
    public string? SegundoNombre { get; set; }

    [Required(ErrorMessage = "El primer apellido es requerido.")]
    [MaxLength(100)]
    public string PrimerApellido { get; set; } = null!;

    [MaxLength(100)]
    public string? SegundoApellido { get; set; }

    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Telefono { get; set; }
}