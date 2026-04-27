using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud.Enums;
namespace VentanillaUnica.Services.Solicitud.Requests;
public class UpdateSolicitudRequest
{
    public int              SolicitudId           { get; set; }
    public int?             FuncionarioAnteriorId { get; set; }
    public int?             FuncionarioNuevoId    { get; set; }
    public EstadoSolicitud  EstadoAnterior        { get; set; }
    public EstadoSolicitud  EstadoNuevo           { get; set; }
    public string?          Observacion           { get; set; }
}