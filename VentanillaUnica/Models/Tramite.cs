namespace VentanillaUnica.Models;

public class Tramite
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int DiasEstimados { get; set; } // para calcular fecha fin estimada
    public bool Activo { get; set; } = true;

    public ICollection<Solicitud> Solicitudes { get; set; } = [];
}