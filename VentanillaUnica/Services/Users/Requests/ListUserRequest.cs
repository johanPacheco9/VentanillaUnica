namespace VentanillaUnica.Services.Users.Requests;

public class ListUserRequest
{
    // Filtro de búsqueda (para buscar por nombre, identificación o email)
    public string? SearchTerm { get; set; }

    // Filtro por rol (opcional)
    public string? RoleFilter { get; set; }

    // Paginación básica
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Para saltar registros en la base de datos
    public int Skip => (Page - 1) * PageSize;
}