using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Tramites;
using VentanillaUnica.Services.Tramites.Requests;

namespace VentanillaUnica.Components.Pages.Tramites;

public partial class Create
{
    [Inject] public TramitesManager   TramitesSvc { get; set; } = null!;
    [Inject] public AppDbContext      DbContext   { get; set; } = null!;
    [Inject] public NavigationManager Nav        { get; set; } = null!;
    
    [Inject] public AuthenticationStateProvider AuthStateProvider  { get; set; } = null!;
    
    private CreateTramiteForm  _form         = new();
    private List<Models.TipoTramite>  _tiposTramite = [];
    private bool               _guardando    = false;
    private string?            _errorMensaje;

    protected async override Task OnInitializedAsync()
    {
        _tiposTramite = await DbContext.TipoTramites
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    private async Task GuardarAsync()
    {
        _guardando    = true;
        _errorMensaje = null;

        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var username = authState.User.Identity?.Name;
            
            var request = new CreateTramiteRequest
            {
                Nombre        = _form.Nombre,
                Descripcion   = _form.Descripcion,
                DiasEstimados = _form.DiasEstimados,
                TipoTramiteId = _form.TipoTramiteId,
                CreadoPor = username ?? "sistema"
            };

            await TramitesSvc.CreateTramite(request);
            Nav.NavigateTo("/tramites");
        }
        catch (Exception)
        {
            _errorMensaje = "Ocurrió un error al crear el trámite. Intente nuevamente.";
        }
        finally
        {
            _guardando = false;
            StateHasChanged();
        }
    }
}