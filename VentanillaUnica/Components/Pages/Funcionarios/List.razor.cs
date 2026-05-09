using Microsoft.AspNetCore.Components;
using VentanillaUnica.Services.Funcionarios;
using VentanillaUnica.Services.Funcionarios.Requests;
namespace VentanillaUnica.Components.Pages.Funcionarios;

public partial class List 
{
    [Inject] private FuncionarioManager FuncionarioService { get; set; } = null!;

    private List<Models.Funcionario> _funcionarios = new();
    private ListFuncionariosRequest _filtros = new();
    private bool _cargando = false;
    private string? _errorMensaje;
    private bool _mostrarModal = false;

    private void CerrarModal() => _mostrarModal = false;

    private async Task OnFuncionarioCreado()
    {
        _mostrarModal = false;
        await FiltrarAsync();
    }
    protected async override Task OnInitializedAsync()
    {
        _filtros.IsActive = null;
        await FiltrarAsync();
    }
    private string? _estadoSeleccionado
    {
        get => _filtros.IsActive?.ToString().ToLower();
        set
        {
            if (string.IsNullOrEmpty(value))
                _filtros.IsActive = null; // Todos
            else
                _filtros.IsActive = bool.Parse(value); // true o false
        }
    }

    private async Task FiltrarAsync()
    {
        try
        {
            _cargando = true;
            _errorMensaje = null;

            _funcionarios = await FuncionarioService.List(_filtros);
        }
        catch (Exception ex)
        {
            _errorMensaje = $"Error al cargar funcionarios: {ex.Message}";
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
        if (_funcionarios.Count >= _filtros.TamañoPagina)
        {
            _filtros.Pagina++;
            await FiltrarAsync();
        }
    }
}