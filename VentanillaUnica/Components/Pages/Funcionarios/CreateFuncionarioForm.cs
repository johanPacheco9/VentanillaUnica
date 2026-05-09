using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Components.Pages.Funcionarios;

public class CreateFuncionarioForm
{
    [Required(ErrorMessage = "El primer nombre es requerido.")]
    [MaxLength(50)]
    public string PrimerNombre { get; set; } = null!;

    [MaxLength(50)]
    public string? SegundoNombre { get; set; }

    [Required(ErrorMessage = "El primer apellido es requerido.")]
    [MaxLength(50)]
    public string PrimerApellido { get; set; } = null!;

    [MaxLength(50)]
    public string? SegundoApellido { get; set; }

    
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Formato de teléfono inválido.")]
    [MaxLength(20)]
    public string? Telefono { get; set; }
}