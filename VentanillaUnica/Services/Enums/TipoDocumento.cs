using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Services.Enums;

public enum TipoDocumento
{
    [Display(Name = "Cédula")]
    CC = 1,
    
    [Display(Name = "Permiso recidencia")]
    PR = 2,
    
    //si hay más tipos de documentos permitidos para los tramites además de la cédula, agregar.
}