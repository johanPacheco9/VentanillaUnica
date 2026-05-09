namespace VentanillaUnica.Models;

public class User : EntityWithTraceability
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; } 
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = null!;
    public string? IdNumber { get; set; }
}