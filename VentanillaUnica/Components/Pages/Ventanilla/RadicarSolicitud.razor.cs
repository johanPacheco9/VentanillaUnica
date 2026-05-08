using Microsoft.AspNetCore.Components;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud.Enums;
using VentanillaUnica.Services.Solicitud.Requests;
namespace VentanillaUnica.Components.Pages.Ventanilla;

public partial class RadicarSolicitud
{
    [Parameter, EditorRequired] public Ciudadano       Ciudadano { get; set; } = null!;
    [Parameter, EditorRequired] public List<Tramite>   Tramites  { get; set; } = [];
    [Parameter] public EventCallback<Models.Solicitud>        OnSolicitudCreada { get; set; }
    [Parameter] public EventCallback OnVolver { get; set; }

    private int     _tramiteId    = 0;
    private bool    _cargando     = false;
    private string? _errorMensaje;
    private string? _descripcion;
    private Origen _origen = Origen.Ventanilla;
    private string? _placa;
    
    private async Task RadicarAsync()
    {
        _cargando     = true;
        _errorMensaje = null;

        try
        {
            var request = new CreateSolicitudRequest
            {
                CiudadanoId   = Ciudadano.Id,
                TipoTramiteId = _tramiteId,
                Observaciones = _descripcion,
                Origen        = _origen,
                Placa         =  _placa
            };

            var solicitud = await SolicitudSvc.CrearAsync(request);
            solicitud.Tramite = Tramites.First(t => t.Id == _tramiteId);
            await OnSolicitudCreada.InvokeAsync(solicitud);
        }
        catch (Exception ex) // <-- Capturamos la variable 'ex'
        {
            _errorMensaje = ex.Message ?? "Ocurrió un error al radicar. Intente nuevamente.";
        }
        finally
        {
            _cargando = false;
            StateHasChanged();
        }
    }
    
    private void OnTramiteChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var id))
        {
            _tramiteId = id;
            var tramite = Tramites.FirstOrDefault(t => t.Id == _tramiteId);
            if (tramite?.TipoTramite?.RequierePlaca != true)
            {
                _placa = null;
            }
            _errorMensaje = null;
        }
    }
}