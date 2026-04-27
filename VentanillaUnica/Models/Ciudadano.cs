using System.ComponentModel.DataAnnotations;
using VentanillaUnica.Services.Enums;

namespace VentanillaUnica.Models;

public class Ciudadano
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrimerNombre { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? SegundoNombre { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrimerApellido { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? SegundoApellido { get; set; }

    [Required]
    [MaxLength(20)]
    public string NumeroDocumento { get; set; } = null!;

    public TipoDocumento TipoDocumento { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Phone]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    // Relaciones
    public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();

    public int MunicipioId { get; set; }
    public virtual Municipio Municipio { get; set; } = null!;
}