namespace VentanillaUnica.Services.Ciudadano.Responses;

public record CiudadanoDetailDto(
    int Id,
    string PrimerNombre,
    string SegundoNombre,
    string PrimerApellido,
    string SegundoApellido,
    string NumeroDocumento,
    string TipoDocumento,
    string? Email,
    string? Telefono,
    string? MunicipioNombre,
    DateTime FechaRegistro
);