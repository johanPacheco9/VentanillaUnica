namespace VentanillaUnica.Services.Importacion.Requests;

public class SolicitudRequestDto
{
    public DateTime Fecha { get; set; }
    public string NumeroRadicado { get; set; } = string.Empty;
    public string NumeroFolio { get; set; } = string.Empty;
    public List<string> Placas { get; set; } = new();
    public string Anotaciones { get; set; } = string.Empty;
}