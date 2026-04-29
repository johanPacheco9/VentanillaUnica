using System.ComponentModel.DataAnnotations.Schema;

namespace VentanillaUnica.Models;

public class Tramite
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int DiasEstimados { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Solicitud> Solicitudes { get; set; } = [];
}