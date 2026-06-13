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
    [Parameter, EditorRequired]
    public Ciudadano Ciudadano { get; set; } = null!;

    [Parameter]
    public List<TramitesByTipoDetailDto> Tramites { get; set; } = [];
    [Parameter]
    public List<TiposTramiteResponseDto> Categorias { get; set; } = [];

    [Parameter]
    public EventCallback<Models.Solicitud> OnSolicitudCreada { get; set; }
    [Parameter]
    public EventCallback OnVolver { get; set; }

    [Inject]
    public AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    [Inject]
    public SolicitudManager SolicitudSvc { get; set; } = null!;

    private List<TramitesByTipoDetailDto> TramitesFiltrados = new();

    private int _tramiteId = 0;
    private bool _cargando = false;
    private string? _errorMensaje;
    private string? _descripcion;
    private Origen _origen = Origen.Ventanilla;
    private List<string> _placas = new() { "" };
    private string _numeroFolio = string.Empty;
    private int _filtroTipoId = -1;
    private bool _eliminarPlaca = false;
    private string? _errorMessage;

    protected override void OnParametersSet()
    {
        if (TramitesFiltrados.Count == 0 && Tramites.Count > 0)
        {
            TramitesFiltrados = Tramites;
        }
    }

    private void OnTipoFiltroChanged(ChangeEventArgs e)
    {
        _filtroTipoId = int.Parse(e.Value?.ToString() ?? "-1");
        _tramiteId = 0;
        _placas = new() { "" };

        if (_filtroTipoId == -1)
        {
            TramitesFiltrados = Tramites;
        }
        else if (_filtroTipoId == 0)
        {
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
        _placas = new() { "" };
    }

    private async Task RadicarAsync()
    {
        if (_tramiteId == 0) return;

        _cargando = true;
        _errorMensaje = null;

        try
        {
            var tramiteSeleccionado = Tramites.FirstOrDefault(t => t.Id == _tramiteId);
            if (tramiteSeleccionado?.RequierePlaca == true)
            {
                var placasValidas = _placas
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Select(p => p.Trim().ToUpper())
                    .ToList();

                if (!placasValidas.Any())
                {
                    _errorMensaje = "Debe ingresar al menos una placa válida";
                    _cargando = false;

                    return;
                }

                // Asignar placas validadas
                _placas = placasValidas;
            }

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var username = authState.User.Identity?.Name;

            var request = new CreateSolicitudRequest
            {
                CiudadanoId = Ciudadano.Id,
                TipoTramiteId = _tramiteId,
                Observaciones = _descripcion,
                Origen = _origen,
                Placas = _placas,
                NumeroFolio = _numeroFolio,
                CreadoPor = username ?? "Sistema"
            };

            var solicitud = await SolicitudSvc.CrearAsync(request);

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

    private void AgregarPlaca()
    {
        _placas.Add("");
    }

    private void RemoverPlaca(int index)
    {
        if (index < 0 || index >= _placas.Count) return;

        if (_placas.Count == 1)
        {
            _errorMessage = "Debe conservar al menos un campo de placa.";
            return;
        }

        _placas.RemoveAt(index);
        _errorMessage = null;
        _eliminarPlaca = false;
    }
    private void ActualizarPlaca(int index, string? valor)
    {
        if (index >= 0 && index < _placas.Count)
        {
            _placas[index] = valor ?? string.Empty;
            _errorMessage = null;
        }
    }
}