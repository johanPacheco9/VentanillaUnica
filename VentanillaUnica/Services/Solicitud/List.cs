using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.Solicitud.Responses;

namespace VentanillaUnica.Services.Solicitud;

public partial class SolicitudManager
{
    public async Task<List<SolicitudSummaryDto>> ListAsync(ListSolicitudesRequest request)
    {
        var query = _dbContext.Solicitudes
            .AsNoTracking()
            .AsQueryable();
        
        if (request.Estado is not null)
            query = query.Where(s => s.Estado == request.Estado);

        if (!string.IsNullOrWhiteSpace(request.NombreCiudadano))
        {
            string terminoBusqueda = $"%{request.NombreCiudadano.Trim()}%";
            
            query = query.Where(s => 
                EF.Functions.ILike(s.Ciudadano.PrimerNombre, terminoBusqueda) ||
                EF.Functions.ILike(s.Ciudadano.SegundoNombre ?? string.Empty, terminoBusqueda) ||
                EF.Functions.ILike(s.Ciudadano.PrimerApellido, terminoBusqueda) ||
                EF.Functions.ILike(s.Ciudadano.SegundoApellido ?? string.Empty, terminoBusqueda));
        }
        if (!string.IsNullOrWhiteSpace(request.NumeroDocumento))
        {
            string terminoBusqueda = $"%{request.NumeroDocumento.Trim()}%";
            
            query = query.Where(s => 
                EF.Functions.ILike(s.Ciudadano.NumeroDocumento, terminoBusqueda));
        }
        if (request.TipoTramiteId.HasValue)
        {
            query = query.Where(s=>s.Tramite.TipoTramiteId ==  request.TipoTramiteId.Value);
        }
        
        if (request.FuncionarioId is not null)
            query = query.Where(s => s.FuncionarioId == request.FuncionarioId);

        if (request.SoloVencidas)
            query = query.Where(s =>
                s.FechaEstimadaFin < DateTime.UtcNow &&
                s.Estado != EstadoSolicitud.Completada &&
                s.Estado != EstadoSolicitud.Rechazada);

        var resultado = await query
            .OrderByDescending(s => s.FechaSolicitud)
            .Skip((request.Pagina - 1) * request.TamañoPagina)
            .Take(request.TamañoPagina)
            .Select(s => new SolicitudSummaryDto(
                s.Id,
                s.NumeroRadicado,
                $"{s.Ciudadano.PrimerNombre} {s.Ciudadano.PrimerApellido}",
                s.Ciudadano.NumeroDocumento,
                s.Tramite.Nombre,
                s.Estado,
                s.FechaSolicitud,
                s.FechaEstimadaFin,
                s.Observaciones,
                s.FuncionarioId,
                s.Funcionario != null 
                    ? $"{s.Funcionario.PrimerNombre} {s.Funcionario.PrimerApellido}" 
                    : null
            ))
            .ToListAsync();

        return resultado;
    }

}