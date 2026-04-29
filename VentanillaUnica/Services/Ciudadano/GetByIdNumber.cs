using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Ciudadano.Requests;

namespace VentanillaUnica.Services.Ciudadano;

public partial class CiudadanoManager
{
    public async Task<Models.Ciudadano?> GetByIdNumber(GetCiudadanoByIdRequest request)
    {
        return await _dbContext.Ciudadanos
            .FirstOrDefaultAsync(c => c.NumeroDocumento == request.IdNumber);
    }
}