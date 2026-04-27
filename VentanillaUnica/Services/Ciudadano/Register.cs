using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.Ciudadano.Requests;

namespace VentanillaUnica.Services.Ciudadano;

public partial class CiudadanoManager
{
    public async Task<Models.Ciudadano> RegistrarAsync(RegistrarCiudadanoRequest request)
    {
        try
        {
            var existe = await _dbContext.Ciudadanos
                .AnyAsync(c => c.NumeroDocumento == request.NumeroIdentificacion);

            if (existe)
                throw new InvalidOperationException(
                    $"Ya existe un ciudadano con el documento {request.NumeroIdentificacion}.");

            var ciudadano = new Models.Ciudadano
            {
                PrimerNombre = request.PrimerNombre,
                SegundoNombre = request?.SegundoNombre,
                PrimerApellido = request.PrimerApellido,
                SegundoApellido = request?.SegundoApellido,
                NumeroDocumento = request.NumeroIdentificacion,
                Email = request?.Email,
                Telefono = request?.Telefono,
                FechaRegistro = DateTime.UtcNow,
                MunicipioId = 1
            };

            _dbContext.Ciudadanos.Add(ciudadano);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Ciudadano registrado: {Id} - {NumeroDocumento}",
                ciudadano.Id, ciudadano.NumeroDocumento);

            return ciudadano;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                $"Error registrando : {e.Message}.");
        }
    }
}