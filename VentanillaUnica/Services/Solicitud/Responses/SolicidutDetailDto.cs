using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Solicitud.Responses;

public record SolicitudDetailDto(
    int Id,
    string CodigoSolicitud,
    DateTime FechaSolicitud,
    EstadoSolicitud Estado,
    DateTime? FechaRespuesta,
    string? Observaciones,
    string? Placa,
    string? NumeroFolio,
    // Información del Ciudadano
    int CiudadanoId,
    string CiudadanoNombreCompleto,
    string CiudadanoNumeroDocumento,
    string CiudadanoEmail,
    string CiudadanoTelefono,
    
    // Información del Funcionario (opcional)
    int? FuncionarioId,
    string? FuncionarioNombreCompleto,
    
    // Información del Trámite
    int TramiteId,
    string TramiteNombre,
    string TramiteDescripcion,
    int? TramiteDiasEstimados
);