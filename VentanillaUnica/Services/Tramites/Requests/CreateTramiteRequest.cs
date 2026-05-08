namespace VentanillaUnica.Services.Tramites.Requests;

public class CreateTramiteRequest
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? DiasEstimados { get; set; }
    public int? TipoTramiteId { get; set; }
}