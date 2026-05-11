using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
using VentanillaUnica.Services.TiposTramites;
using VentanillaUnica.Services.TiposTramites.Responses;
using VentanillaUnica.Services.Tramites;
using VentanillaUnica.Services.Tramites.Requests;
using VentanillaUnica.Services.Tramites.Responses;

namespace VentanillaUnica.Components.Pages.Ventanilla;

public partial class Register
{
    [Inject] public AppDbContext DbContext { get; set; } = null!;
    [Inject]
    public TramitesManager TramitesManager { get; set; } = null!;
    [Inject] public TiposTramitesManager TiposManager { get; set; } = null!;
    private int    _paso     = 1;
    private void VolverPaso1() => _paso = 1;

    private Ciudadano?        _ciudadano;
    private Models.Solicitud?        _solicitudCreada;
    private List<TramitesByTipoDetailDto> _catalogoTramites = [];
    private List<TiposTramiteResponseDto> _categorias = [];

    protected async override Task OnInitializedAsync()
    {
        var TipoTramitesRequest = new GetTramitesByTipoRequest
        {
            TipoTramiteId = 0
        };
       
        _categorias = await TiposManager.List();
        _catalogoTramites = await TramitesManager.GetByTipoTramites(TipoTramitesRequest);
    }


    private string GetEstado(int numeroPaso) =>
        numeroPaso < _paso  ? "done"   :
        numeroPaso == _paso ? "active" :
        "pending";

    private void OnCiudadanoListo(Ciudadano ciudadano)
    {
        _ciudadano = ciudadano;
        _paso = 2;
    }

    public void OnSolicitudCreada(Models.Solicitud solicitud)
    {
        _solicitudCreada = solicitud;
        _paso = 3;
    }

    private void NuevaSolicitud()
    {
        _ciudadano       = null;
        _solicitudCreada = null;
        _paso            = 1;
    }
}