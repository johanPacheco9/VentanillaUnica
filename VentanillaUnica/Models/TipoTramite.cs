namespace VentanillaUnica.Models;

public class TipoTramite : EntityWithTraceability
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public bool? RequierePlaca { get; set; }
    
    public ICollection<Tramite> Tramites { get; set; } = [];
}