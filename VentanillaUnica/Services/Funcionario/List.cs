using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Services.Funcionario.Requests;
using VentanillaUnica.Models;

namespace VentanillaUnica.Services.Funcionario;

public partial class FuncionarioManager
{
    public async Task<List<Models.Funcionario>> List(ListFuncionariosRequest request)
    {
        IQueryable<Models.Funcionario> query = _dbContext.Funcionario.AsNoTracking();

        if (request.IsActive.HasValue)
        {
            query = query.Where(f => f.Activo == request.IsActive.Value);
        }
        query = query
            .Skip((request.Pagina - 1) * request.TamañoPagina)
            .Take(request.TamañoPagina);

        return await query.ToListAsync();
    }
}