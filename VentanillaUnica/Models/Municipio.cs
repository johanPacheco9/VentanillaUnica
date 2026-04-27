namespace VentanillaUnica.Models;

using System.Collections.Generic;

public class Municipio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Code { get; set; } 
    public string? Departmento { get; set; }
    
    // Relación: Un municipio tiene muchos usuarios
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}