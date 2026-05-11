namespace VentanillaUnica.Services.Tramites.Responses;

public record TramitesByTipoDetailDto
(
    int Id,
    string Name,
    int? DiasEstimados,
    bool? RequierePlaca,
    int? TipoTramiteId
);