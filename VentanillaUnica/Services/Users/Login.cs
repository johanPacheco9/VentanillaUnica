using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Users.Requests;

namespace VentanillaUnica.Services.Users;

public partial class UserManager
{
    public async Task<User?> ValidateUser(LoginRequest request)
    {
        try
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.UserName || u.Username == request.UserName.ToUpper());

            if (user == null) return null;
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            
            if (result == PasswordVerificationResult.Failed) return null;

            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al validar usuario");
            return null;
        }
    }
}