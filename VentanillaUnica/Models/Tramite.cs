using System.ComponentModel.DataAnnotations.Schema;

namespace VentanillaUnica.Models;

public class Tramite : EntityWithTraceability
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int? DiasEstimados { get; set; }
    public bool Activo { get; set; } = true;
    
    public int? TipoTramiteId { get; set; }
    
    public TipoTramite? TipoTramite { get; set; }

    public ICollection<Solicitud> Solicitudes { get; init; } = [];
}