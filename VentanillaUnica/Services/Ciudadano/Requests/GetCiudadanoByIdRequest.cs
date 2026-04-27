using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Services.Ciudadano.Requests;

public class GetCiudadanoByIdRequest
{
    [Required(ErrorMessage = "El número de documento es requerido.")]
    [MinLength(5,  ErrorMessage = "Mínimo 5 caracteres.")]
    [MaxLength(20, ErrorMessage = "Máximo 20 caracteres.")]
    public string IdNumber { get; set; } = null!;
}