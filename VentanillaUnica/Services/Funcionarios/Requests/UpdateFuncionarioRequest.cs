namespace VentanillaUnica.Services.Funcionarios.Requests;

public class UpdateFuncionarioRequest
{
    public int Id { get; set; }
    public string PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = string.Empty;
    public string? SegundoApellido { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public bool Activo { get; set; }
    
    // Trazabilidad
    public string? ModificadoPor { get; set; }
}