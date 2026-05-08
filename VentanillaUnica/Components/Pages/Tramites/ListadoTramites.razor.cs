using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Tramites;
using VentanillaUnica.Services.Tramites.Requests;

namespace VentanillaUnica.Components.Pages.Tramites;

public partial class ListadoTramites
{
    [Inject] public TramitesManager   TramitesSvc { get; set; } = null!;
    [Inject] public AppDbContext      DbContext   { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private List<Tramite>      _tramites     = [];
    private List<TipoTramite>  _tiposTramite = [];
    private ListTramitesRequest _filtros     = new();
    private bool               _cargando     = true;
    private string?            _errorMensaje;
    private bool               _mostrarModal = false;

    private string? _estadoSeleccionado
    {
        get => _filtros.Activo?.ToString().ToLower();
        set => _filtros.Activo = string.IsNullOrEmpty(value) ? null : bool.Parse(value);
    }

    protected async override Task OnInitializedAsync()
    {
        _tiposTramite = await DbContext.TipoTramites
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync();

        await FiltrarAsync();
    }

    private async Task FiltrarAsync()
    {
        _cargando     = true;
        _errorMensaje = null;
        try
        {
            _tramites = await TramitesSvc.GetTramites(_filtros);
        }
        catch (Exception)
        {
            _errorMensaje = "Error al cargar los trámites.";
        }
        finally
        {
            _cargando = false;
            StateHasChanged();
        }
    }

    private async Task PaginaAnterior()
    {
        if (_filtros.Pagina > 1)
        {
            _filtros.Pagina--;
            await FiltrarAsync();
        }
    }

    private async Task PaginaSiguiente()
    {
        if (_tramites.Count >= _filtros.TamañoPagina)
        {
            _filtros.Pagina++;
            await FiltrarAsync();
        }
    }

    private void CerrarModal() => _mostrarModal = false;

    private async Task NavigateCreatePage()
    {
        NavigationManager.NavigateTo("/tramites/nuevo");
    }
}