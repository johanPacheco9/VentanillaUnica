using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Funcionario.Requests;

namespace VentanillaUnica.Services.Funcionario;

public partial class FuncionarioManager
{
    public async Task<Models.Funcionario> RegistrarAsync(RegistrarFuncionarioRequest request)
    {
        var existe = await _dbContext.Funcionario
            .AnyAsync(f => f.Email == request.Email);

        if (existe)
            throw new InvalidOperationException(
                $"Ya existe un funcionario con el email {request.Email}.");

        var funcionario = new Models.Funcionario
        {
            PrimerNombre    = request.PrimerNombre,
            SegundoNombre   = request.SegundoNombre,
            PrimerApellido  = request.PrimerApellido,
            SegundoApellido = request.SegundoApellido,
            Email           = request.Email,
            Telefono        = request.Telefono,
            Activo          = true
        };

        _dbContext.Funcionario.Add(funcionario);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Funcionario registrado: {Id} - {Email}",
            funcionario.Id, funcionario.Email);

        return funcionario;
    }
}