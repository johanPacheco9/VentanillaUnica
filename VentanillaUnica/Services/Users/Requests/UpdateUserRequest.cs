namespace VentanillaUnica.Services.Users.Requests;

public class UpdateUserRequest
{
    // El ID es obligatorio para saber qué registro actualizar en la DB
    public int Id { get; set; }

    // El nombre de usuario suele ser de solo lectura en ediciones, 
    // pero lo incluimos para mostrarlo en la vista
    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? IdNumber { get; set; }

    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = null!;

    // Si este campo viene lleno, el Manager actualizará la contraseña
    public string? Password { get; set; }

    // Campo de trazabilidad heredado de la lógica de EntityWithTraceability
    public string? ModificadoPor { get; set; }
}