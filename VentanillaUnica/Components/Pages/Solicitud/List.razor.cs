using Microsoft.AspNetCore.Components;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Solicitud;
using VentanillaUnica.Services.Solicitud.Enums;
using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.Solicitud.Responses;
using VentanillaUnica.Services.TiposTramites;
using VentanillaUnica.Services.TiposTramites.Responses;
using VentanillaUnica.Services.Tramites;
using VentanillaUnica.Services.Tramites.Requests;

namespace VentanillaUnica.Components.Pages.Solicitud;

public partial class List
{
    [Inject] public SolicitudManager SolicitudSvc { get; set; } = null!;
    
    [Inject] public TramitesManager  TramiteMngr { get; set; } =  null!;

    [Inject]
    public TiposTramitesManager TipoTramiteMngr { get; set; } = null!;

    private List<SolicitudSummaryDto> _solicitudes  = [];
    private bool                   _cargando     = true;
    private string?                _errorMensaje;

    private readonly ListSolicitudesRequest _filtros = new();

    private readonly ListTramitesRequest _listTramitesRequest = new();
    
    private List<TiposTramiteResponseDto> _tiposTramites = [];
    
    protected async override Task OnInitializedAsync()
    {
        await CargarAsync();
        await GetTipoTramites();
    }

    private async Task CargarAsync()
    {
        _cargando = true;
        try
        {
            _solicitudes = await SolicitudSvc.ListAsync(_filtros);
        }
        catch (Exception)
        {
            _errorMensaje = "Error al cargar las solicitudes.";
        }
        finally
        {
            _cargando = false;
        }
    }

    private async Task FiltrarAsync()
    {
        _filtros.Pagina = 1;
        await CargarAsync();
    }

    private bool EsVencida(SolicitudSummaryDto s) =>
        s.FechaEstimadaFin.HasValue &&
        s.FechaEstimadaFin < DateTime.UtcNow &&
        s.Estado != EstadoSolicitud.Completada &&
        s.Estado != EstadoSolicitud.Rechazada;
    
    private async Task GetTramites()
    {
        var response = TramiteMngr.GetTramites(_listTramitesRequest);
    }

    private async Task GetTipoTramites()
    {
        var response = await TipoTramiteMngr.List();
        if (response.Any())
        {
            _tiposTramites = response;
        }
    }

    private async Task PaginaAnterior()
    {
        if (_filtros.Pagina > 1)
        {
            _filtros.Pagina--;
            await CargarAsync();
        }
    }

    private async Task PaginaSiguiente()
    {
        _filtros.Pagina++;
        await CargarAsync();
    }
    
    private static string GetBadgeEstado(EstadoSolicitud estado) => estado switch
    {
        EstadoSolicitud.Pendiente  => "bg-warning text-dark",
        EstadoSolicitud.Asignada   => "bg-info text-dark",
        EstadoSolicitud.EnProceso  => "bg-primary",
        EstadoSolicitud.Completada => "bg-success",
        EstadoSolicitud.Rechazada  => "bg-danger",
        _                          => "bg-secondary"
    };
}