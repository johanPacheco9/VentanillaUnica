using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Services.Solicitud.Enums;

public enum Origen
{
    [Display(Name = "Correo")]
    Correo = 1,
    
    [Display(Name = "Ventanilla")]
    Ventanilla = 2
}