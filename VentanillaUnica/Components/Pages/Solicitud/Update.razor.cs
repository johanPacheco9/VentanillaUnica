using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Enums;
using VentanillaUnica.Services.Gestion;
using VentanillaUnica.Services.Gestion.Requests;
using VentanillaUnica.Services.Solicitud;
using VentanillaUnica.Services.Solicitud.Enums;
using VentanillaUnica.Services.Solicitud.Requests;
using VentanillaUnica.Services.Solicitud.Responses;

namespace VentanillaUnica.Components.Pages.Solicitud;

public partial class Update
{
    [Inject]
    public SolicitudManager SolicitudSvc { get; set; } = null!;
    [Inject] public GestionManager  GestionSolicitudSvc { get; set; } = null!;
    [Inject] public AppDbContext      DbContext     { get; set; } = null!;
    [Inject] public NavigationManager Nav          { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    private SolicitudDetailDto?    _solicitud;
    private List<GestionSolicitud> _gestiones    = [];
    private List<Models.Funcionario>      _funcionarios = [];
    private UpdateSolicitudForm    _form         = new();
    private bool                   _cargando     = true;
    private bool                   _guardando    = false;
    private string?                _errorMensaje;
    private string?                _exitoMensaje;
      
    protected async override Task OnInitializedAsync()
    {
        await CargarAsync();
    }

    private async Task CargarAsync()
    {
        _cargando = true;
        try
        {
            _solicitud = await SolicitudSvc.GetById(new GetSolicitudByIdRequest { Id = Id });

            _funcionarios = await DbContext.Funcionario
                .AsNoTracking()
                .Where(f => f.Activo)
                .OrderBy(f => f.PrimerApellido)
                .ToListAsync();

            _gestiones = await SolicitudSvc.GetGestionesAsync(Id);

            _form = new UpdateSolicitudForm
            {
                FuncionarioId = _solicitud.FuncionarioId,
                Estado        = _solicitud.Estado
            };
        }
        catch (InvalidOperationException)
        {
            Nav.NavigateTo("/admin/solicitudes");
        }
        catch (Exception)
        {
            _errorMensaje = "Error al cargar la solicitud.";
        }
        finally
        {
            _cargando = false;
        }
    }

    private async Task GuardarAsync()
    {
        _guardando = true;
        _errorMensaje = null;
        _exitoMensaje = null;

        try
        {
            var request = new CreateGestionRequest
            {
                SolicitudId = Id,
                NuevoEstado = _form.Estado,
                NuevoFuncionarioAsignado = _form.FuncionarioId,
                Observacion = _form.Observacion,
                RealizadoPor = "usuario_actual" 
            };

            await GestionSolicitudSvc.Create(request);

            _solicitud = await SolicitudSvc.GetById(new GetSolicitudByIdRequest { Id = Id });
            _gestiones = await SolicitudSvc.GetGestionesAsync(Id);
            _exitoMensaje = "Solicitud actualizada correctamente.";
        }
        catch (Exception ex)
        {
            _errorMensaje = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            _guardando = false;
            StateHasChanged();
        }
    }


    private bool EsVencida() =>
        _solicitud?.FechaRespuesta.HasValue == true &&
        _solicitud.FechaRespuesta < DateTime.UtcNow &&
        _solicitud.Estado != EstadoSolicitud.Completada &&
        _solicitud.Estado != EstadoSolicitud.Rechazada;

    private static string GetBadgeEstado(EstadoSolicitud estado) => estado switch
    {
        EstadoSolicitud.Pendiente  => "bg-warning text-dark",
        EstadoSolicitud.Asignada   => "bg-info text-dark",
        EstadoSolicitud.EnProceso  => "bg-primary",
        EstadoSolicitud.Completada => "bg-success",
        EstadoSolicitud.Rechazada  => "bg-danger",
        _                          => "bg-secondary"
    };
    
    private string DiasEnTramite
    {
        get
        {
            if (_solicitud == null) return "N/A";
            var dias = (DateTime.Now - _solicitud.FechaSolicitud).Days;
        
            if (dias == 0) return "Hoy mismo";
            if (dias == 1) return "1 día";
            return $"{dias} días";
        }
    }
}