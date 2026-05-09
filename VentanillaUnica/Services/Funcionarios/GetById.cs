using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
namespace VentanillaUnica.Services.Funcionarios;

public partial class FuncionarioManager
{
    public async Task<Funcionario?> GetById(int id)
    {
        var funcionario = await _dbContext.Funcionario.FirstOrDefaultAsync(s => s.Id == id);
        if (funcionario is null) throw new KeyNotFoundException($"No se encontró el funcionario solicitado.");

        return funcionario;
    }
}