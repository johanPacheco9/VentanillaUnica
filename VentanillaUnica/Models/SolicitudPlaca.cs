namespace VentanillaUnica.Models;

public class SolicitudPlaca
{
    public int    Id          { get; set; }
    public int    SolicitudId { get; set; }
    public Solicitud Solicitud { get; set; } = null!;
    public string Placa       { get; set; } = null!;
}