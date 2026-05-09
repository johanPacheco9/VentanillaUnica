namespace VentanillaUnica.Services.Users.Requests;

public class CreateUserRequest
{  
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = null!;
    public string? IdNumber { get; set; }

    public string CreadoPor { get; set; } = null!;
}