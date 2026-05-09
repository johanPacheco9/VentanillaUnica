using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.Funcionarios.Requests;
namespace VentanillaUnica.Services.Funcionarios;

public partial class FuncionarioManager
{
    public async Task Update(UpdateFuncionarioRequest request)
    {
        var funcionario = await _dbContext.Funcionario.FirstOrDefaultAsync(s => s.Id == request.Id);

        if (funcionario is null) 
            throw new KeyNotFoundException($"No se encontró al funcionario con ID {request.Id}.");
        
        funcionario.PrimerNombre = request.PrimerNombre;
        funcionario.SegundoNombre = request.SegundoNombre;
        funcionario.PrimerApellido = request.PrimerApellido;
        funcionario.SegundoApellido = request.SegundoApellido;
        funcionario.Email = request.Email;
        funcionario.Telefono = request.Telefono;
        funcionario.Activo = request.Activo;
        funcionario.ModificadoPor = request.ModificadoPor;
        funcionario.FechaModificacion = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
    }
}