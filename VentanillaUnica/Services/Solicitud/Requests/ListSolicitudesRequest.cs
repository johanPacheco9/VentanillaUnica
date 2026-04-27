using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Solicitud.Requests;

public class ListSolicitudesRequest
{
    public EstadoSolicitud? Estado      { get; set; }
    public int?             FuncionarioId { get; set; }
    public bool             SoloVencidas  { get; set; } = false;
    public int              Pagina        { get; set; } = 1;
    public int              TamañoPagina  { get; set; } = 20;
}