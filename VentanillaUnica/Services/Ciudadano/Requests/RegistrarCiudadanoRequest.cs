using System.ComponentModel.DataAnnotations;

namespace VentanillaUnica.Services.Ciudadano.Requests;

public class RegistrarCiudadanoRequest
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    [MaxLength(100)]
    public string PrimerNombre { get; set; } = null!;
    
    public string? SegundoNombre { get; set; }
    
    [Required(ErrorMessage = "El primer apellido es requerido.")]
    [MaxLength(100)]
    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; } 
    
    [Required(ErrorMessage = "El número de documento es requerido.")]
    [MinLength(5,  ErrorMessage = "Mínimo 5 caracteres.")]
    [MaxLength(20, ErrorMessage = "Máximo 20 caracteres.")]
    public string NumeroIdentificacion { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(150)]
    public string? Email { get; set; } 

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    public string Telefono { get; set; } = string.Empty;
}