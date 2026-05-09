namespace VentanillaUnica.Services.Funcionarios.Requests;

public class ListFuncionariosRequest
{
    public bool? IsActive { get; set; } 
    public int              Pagina        { get; set; } = 1;
    public int              TamañoPagina  { get; set; } = 7;
}
