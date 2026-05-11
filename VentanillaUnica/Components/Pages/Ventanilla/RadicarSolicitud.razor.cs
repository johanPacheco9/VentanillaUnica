using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Solicitud;
using VentanillaUnica.Services.Solicitud.Enums;
using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.TiposTramites.Responses;
using VentanillaUnica.Services.Tramites.Responses;

namespace VentanillaUnica.Components.Pages.Ventanilla;

public partial class RadicarSolicitud
{
    [Parameter, EditorRequired] public Ciudadano Ciudadano { get; set; } = null!;
    
    // Eliminamos la lista duplicada de 'Tramite' y dejamos solo los DTOs
    [Parameter] public List<TramitesByTipoDetailDto> Tramites { get; set; } = [];
    [Parameter] public List<TiposTramiteResponseDto> Categorias { get; set; } = [];  
    
    [Parameter] public EventCallback<Models.Solicitud> OnSolicitudCreada { get; set; }
    [Parameter] public EventCallback OnVolver { get; set; }
    
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    [Inject] public SolicitudManager SolicitudSvc { get; set; } = null!;

    private List<TramitesByTipoDetailDto> TramitesFiltrados = new();

    private int     _tramiteId    = 0;
    private bool    _cargando     = false;
    private string? _errorMensaje;
    private string? _descripcion;
    private Origen  _origen       = Origen.Ventanilla;
    private string? _placa;
    private string _numeroFolio = string.Empty;
    private int     _filtroTipoId = -1;

    protected override void OnParametersSet()
    {
        // Inicializamos la lista filtrada con lo que llega del padre
        if (TramitesFiltrados.Count == 0 && Tramites.Count > 0)
        {
            TramitesFiltrados = Tramites;
        }
    }

    private void OnTipoFiltroChanged(ChangeEventArgs e)
    {
        _filtroTipoId = int.Parse(e.Value?.ToString() ?? "-1");
        _tramiteId = 0; 
        _placa = null;

        if (_filtroTipoId == -1)
        {
            TramitesFiltrados = Tramites;
        }
        else if (_filtroTipoId == 0)
        {
            // Filtramos los que tienen TipoTramiteId nulo (5to parámetro del DTO)
            TramitesFiltrados = Tramites.Where(t => t.TipoTramiteId == null || t.TipoTramiteId == 0).ToList();
        }
        else
        {
            TramitesFiltrados = Tramites.Where(t => t.TipoTramiteId == _filtroTipoId).ToList();
        }
    }

    private void OnTramiteChanged(ChangeEventArgs e)
    {
        _tramiteId = int.Parse(e.Value?.ToString() ?? "0");
        _placa = string.Empty;
    }

    private async Task RadicarAsync()
    {
        if (_tramiteId == 0) return;

        _cargando = true;
        _errorMensaje = null;

        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var username = authState.User.Identity?.Name;
            
            var request = new CreateSolicitudRequest
            {
                CiudadanoId   = Ciudadano.Id,
                TipoTramiteId = _tramiteId,
                Observaciones = _descripcion,
                Origen        = _origen,
                Placa         = _placa,
                NumeroFolio = _numeroFolio,
                CreadoPor     = username ?? "Sistema"
            };

            var solicitud = await SolicitudSvc.CrearAsync(request);
            
            // Asignamos el trámite desde nuestra lista local para el resumen final
            var tInfo = Tramites.First(t => t.Id == _tramiteId);
            // Si tu modelo Solicitud espera un objeto Tramite, asegúrate de mapearlo o manejarlo en el resumen
            
            await OnSolicitudCreada.InvokeAsync(solicitud);
        }
        catch (Exception ex)
        {
            _errorMensaje = "Error: " + ex.Message;
        }
        finally
        {
            _cargando = false;
        }
    }
}