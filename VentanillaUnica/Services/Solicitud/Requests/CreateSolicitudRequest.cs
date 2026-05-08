using System.ComponentModel.DataAnnotations;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Solicitud.Requests;

public class CreateSolicitudRequest
{
    [Required]
    public int CiudadanoId { get; set; }

    [Required]
    public int TipoTramiteId { get; set; }

    public string? Observaciones { get; set; }
    
    [Required (ErrorMessage = "El origen de la solicitud es requerido")]
    public Origen Origen { get; set; }
    
    public string? Placa { get; set; }
}