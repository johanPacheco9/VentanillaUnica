using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
namespace VentanillaUnica.Services.Users;

public partial class UserManager
{
    public async Task<User> GetById(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
        if (user is null)
        {
            throw new KeyNotFoundException($"No se encontró un usuario con el ID {id}.");
        }
        return user;
    }
}