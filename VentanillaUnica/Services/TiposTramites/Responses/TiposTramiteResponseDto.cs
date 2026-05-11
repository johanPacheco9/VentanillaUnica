namespace VentanillaUnica.Services.TiposTramites.Responses;

public record TiposTramiteResponseDto
(
    int Id,
    string Name,
    bool? RequierePlaca
    );