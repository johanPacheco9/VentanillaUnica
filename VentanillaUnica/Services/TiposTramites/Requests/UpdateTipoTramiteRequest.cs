using System.ComponentModel.DataAnnotations;

namespace VentanillaUnica.Services.TiposTramites.Requests;

public class UpdateTipoTramiteRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre es demasiado largo")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool RequierePlaca { get; set; }
    
    public string? ModificadoPor { get; set; }
}