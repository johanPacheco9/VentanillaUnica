using System.ComponentModel.DataAnnotations;

namespace VentanillaUnica.Models;

public class Funcionario
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El primer nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El primer nombre no puede exceder los 50 caracteres")]
    public string PrimerNombre { get; set; } = string.Empty;
    
    [MaxLength(50, ErrorMessage = "El segundo nombre no puede exceder los 50 caracteres")]
    public string? SegundoNombre { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El primer apellido es obligatorio")]
    [MaxLength(50, ErrorMessage = "El primer apellido no puede exceder los 50 caracteres")]
    public string PrimerApellido { get; set; } = string.Empty;
    
    [MaxLength(50, ErrorMessage = "El segundo apellido no puede exceder los 50 caracteres")]
    public string? SegundoApellido { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El email es obligatorio")]
    [MaxLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string? Email { get; set; } = string.Empty;
    
    [MaxLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    public string? Telefono { get; set; }
    
    public bool Activo { get; set; } = true;

    // Relaciones
    public ICollection<Solicitud> Solicitudes { get; set; } = [];
    
    // Propiedad de conveniencia para nombre completo
    public string NombreCompleto => $"{PrimerNombre} + {PrimerApellido} ".Trim();
}