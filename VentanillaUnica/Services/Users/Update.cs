using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.Users.Requests;
namespace VentanillaUnica.Services.Users;

public partial class UserManager
{
    public async Task Update(UpdateUserRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == request.Id);
    
        if (user is null) 
            throw new KeyNotFoundException($"No se encontró al usuario con ID {request.Id}.");
        
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.IdNumber = request.IdNumber;
        user.PhoneNumber = request.PhoneNumber;
        user.Role = request.Role;
        user.FechaModificacion = DateTime.UtcNow;
        user.ModificadoPor = request.ModificadoPor;
        
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.Password = _passwordHasher.HashPassword(user, request.Password);
        }
        
        await _dbContext.SaveChangesAsync();
    }
}