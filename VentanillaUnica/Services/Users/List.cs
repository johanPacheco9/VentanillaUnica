using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Users.Requests;
namespace VentanillaUnica.Services.Users;

public partial class  UserManager
{
    public async Task<List<User>> GetUsersAsync(ListUserRequest request)
    {
        var query = _dbContext.Users.AsQueryable();

        // 1. Filtro de búsqueda (usar Trim() para evitar espacios accidentales)
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim().ToUpper();
            query = query.Where(u => u.Username.ToUpper().Contains(term) || 
                                     (u.Email != null && u.Email.ToUpper().Contains(term)) || 
                                     (u.IdNumber != null && u.IdNumber.Contains(term)));
        }

        // 2. Filtro de Rol: Comparación EXACTA
        // Si mandas "Admin", solo traerá los que sean estrictamente "Admin"
        if (!string.IsNullOrWhiteSpace(request.RoleFilter))
        {
            query = query.Where(u => u.Role == request.RoleFilter);
        }

        // 3. Ejecución de la consulta con paginación
        // Nota: El OrderBy es crucial antes del Skip/Take
        return await query
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName) 
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();
    }
}