using System.ComponentModel.DataAnnotations;
namespace VentanillaUnica.Services.Users.Requests;

public class LoginRequest
{
    /// <summary>
    /// Puede ser el correo electrónico o el nombre de usuario
    /// </summary>
    [Required(ErrorMessage = "El usuario o correo es obligatorio")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;
}