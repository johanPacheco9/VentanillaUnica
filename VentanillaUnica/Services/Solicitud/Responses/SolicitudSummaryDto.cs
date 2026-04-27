using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Solicitud.Responses;
public record SolicitudSummaryDto(
    int Id,
    string NumeroRadicado,
    string CiudadanoNombreCompleto,
    string CiudadanoNumeroDocumento,
    string TramiteNombre,
    EstadoSolicitud Estado,
    DateTime FechaSolicitud,
    DateTime? FechaEstimadaFin,
    string? Observaciones,
    int? FuncionarioId,
    string? FuncionarioNombreCompleto
);
