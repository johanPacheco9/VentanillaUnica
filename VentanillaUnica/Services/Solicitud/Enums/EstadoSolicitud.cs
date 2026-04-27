using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Services.Solicitud.Enums;

public enum EstadoSolicitud
{
    [Display(Name = "Pendiente")]
    Pendiente = 0,
    
    [Display(Name = "Asignada")]
    Asignada = 1,
    
    [Display(Name = "En Proceso")]
    EnProceso = 2,
    
    [Display(Name = "Completada")]
    Completada = 3,
    
    [Display(Name = "Rechazada")]
    Rechazada = 4
}