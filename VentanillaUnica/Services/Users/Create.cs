using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Users.Requests;

namespace VentanillaUnica.Services.Users;

public partial class UserManager
{
    public async Task<User> Create(CreateUserRequest request)
    {
        try
        {
            var exist = await _dbContext.Users
                .AnyAsync(s => s.IdNumber == request.IdNumber);

            if (exist)
                throw new Exception("El número de identificación ya está registrado.");
            
            var user = new User
            {
                Username = request.UserName.ToUpper(),
                Email = request.Email,
                FirstName = request.FirstName ?? "",
                LastName = request.LastName,
                IdNumber = request.IdNumber,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role,
                CreadoPor = request.CreadoPor,
                FechaCreacion = DateTime.UtcNow,
            };
            user.Password = _passwordHasher.HashPassword(user, request.Password);
           
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear usuario");
            throw;
        }
    }
}