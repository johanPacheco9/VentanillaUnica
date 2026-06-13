using System.ComponentModel.DataAnnotations;

namespace VentanillaUnica.Services.Enums;

public enum TipoDocumento
{
    [Display(Name = "Cédula")]
    CC = 1,
    
    [Display(Name = "Permiso residencia")]
    PR = 2,

    [Display(Name = "Pasaporte")]
    PAS = 3,

    [Display(Name = "NIT")]
    NIT = 4
}