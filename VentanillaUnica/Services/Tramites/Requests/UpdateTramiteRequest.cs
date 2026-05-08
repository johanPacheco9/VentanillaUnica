namespace VentanillaUnica.Services.Tramites.Requests;

public class UpdateTramiteRequest
{
    public int? Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? DiasEstimados { get; set; }
    public int? TipoTramiteId { get; set; }
    public bool Activo { get; set; }
}