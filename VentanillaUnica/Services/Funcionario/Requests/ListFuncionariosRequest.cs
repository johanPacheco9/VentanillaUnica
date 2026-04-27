namespace VentanillaUnica.Services.Funcionario.Requests;

public class ListFuncionariosRequest
{
    public bool? IsActive { get; set; } 
    public int              Pagina        { get; set; } = 1;
    public int              TamañoPagina  { get; set; } = 7;
}
