namespace VentanillaUnica.Services.Tramites.Requests;

public class ListTramitesRequest
{
    public bool?   Activo        { get; set; }
    public int?    TipoTramiteId { get; set; }
    public int     Pagina        { get; set; } = 1;
    public int     TamañoPagina  { get; set; } = 20;
}