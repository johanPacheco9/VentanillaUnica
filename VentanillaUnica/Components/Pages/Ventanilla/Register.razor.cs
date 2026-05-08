using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Data;
using VentanillaUnica.Models;

namespace VentanillaUnica.Components.Pages.Ventanilla;

public partial class Register
{
    [Inject] public AppDbContext DbContext { get; set; } = null!;

    private int    _paso     = 1;
    private void VolverPaso1() => _paso = 1;

    private Ciudadano?        _ciudadano;
    private Models.Solicitud?        _solicitudCreada;
    private List<Tramite>     _tiposTramite = [];

    protected async override Task OnInitializedAsync()
    {
        _tiposTramite = await DbContext.Tramites
            .Include(t => t.TipoTramite)
            .Where(t => t.Activo)
            .OrderBy(t => t.Nombre)
            .ToListAsync();
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