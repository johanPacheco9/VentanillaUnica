namespace VentanillaUnica.Services.Users.Responses;

public class LoginResponseDto
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = "";
    public string Token { get; set; } = "";
}