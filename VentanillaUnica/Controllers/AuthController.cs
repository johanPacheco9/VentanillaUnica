using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VentanillaUnica.Services.Users;
using VentanillaUnica.Services.Users.Requests;

namespace VentanillaUnica.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager _userManager;

    public AuthController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request) // Recibe el objeto del formulario
    {
        var user = await _userManager.ValidateUser(request); 

        if (user == null) 
        {
            return Redirect("/?error=true");
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FirstName", user.FirstName ?? ""),
            new Claim("LastName", user.LastName ?? ""),
            new Claim("UserId", user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));

        return Redirect("/Solicitudes"); 
    }
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}