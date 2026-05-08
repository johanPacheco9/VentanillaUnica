using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.Solicitud.Responses;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Generics.GenericDisplayName;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager
{
    public async Task<SolicitudDetailDto> GetById(GetSolicitudByIdRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        
        if (request.Id <= 0)
            throw new ArgumentException("El ID debe ser mayor a 0", nameof(request.Id));
        
        try
        {
            var solicitud = await _dbContext.Solicitudes
                .Where(s => s.Id == request.Id)
                .Select(s => new SolicitudDetailDto(
                    Id: s.Id,
                    CodigoSolicitud: s.NumeroRadicado,
                    FechaSolicitud: s.FechaSolicitud,
                    Estado: s.Estado,
                    FechaRespuesta: s.FechaEstimadaFin,
                    Observaciones: s.Observaciones,
                    s.Placa ?? null,
                    CiudadanoId: s.Ciudadano.Id,
                    CiudadanoNombreCompleto: s.Ciudadano.PrimerNombre + " " + s.Ciudadano.PrimerApellido,
                    CiudadanoNumeroDocumento: s.Ciudadano.NumeroDocumento,
                    CiudadanoEmail: s.Ciudadano.Email ?? string.Empty,
                    CiudadanoTelefono: s.Ciudadano.Telefono ?? string.Empty,
                    FuncionarioId: s.Funcionario != null ? s.Funcionario.Id : (int?)null,
                    FuncionarioNombreCompleto: s.Funcionario != null ? 
                        s.Funcionario.PrimerNombre + " " + s.Funcionario.PrimerApellido : null,
                    TramiteId: s.Tramite.Id,
                    TramiteNombre: s.Tramite.Nombre,
                    TramiteDescripcion: s.Tramite.Descripcion ?? string.Empty,
                    TramiteDiasEstimados: s.Tramite.DiasEstimados ?? null
                ))
                .FirstOrDefaultAsync();
            if (solicitud == null)
                throw new InvalidOperationException($"No se encontró la solicitud con ID: {request.Id}");
            
            _logger.LogInformation("Solicitud obtenida: {Codigo} - Estado: {Estado}", 
                solicitud.CodigoSolicitud, solicitud.Estado);
            
            return solicitud;
        }
        catch (Exception ex) when (ex is InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener solicitud {SolicitudId}", request.Id);
            throw new InvalidOperationException($"Error al obtener la solicitud: {ex.Message}", ex);
        }
    }
}