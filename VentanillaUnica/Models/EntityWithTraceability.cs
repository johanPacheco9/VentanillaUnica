namespace VentanillaUnica.Models;

public abstract class EntityWithTraceability
{
    public string? CreadoPor { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? ModificadoPor { get; set; }
}