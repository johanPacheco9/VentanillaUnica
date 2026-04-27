using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Components;
using VentanillaUnica.Data;
using VentanillaUnica.Services.Ciudadano;
using VentanillaUnica.Services.Funcionario;
using VentanillaUnica.Services.Gestion;
using VentanillaUnica.Services.Solicitud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=VentanillaUnica;Username=postgres;Password=1098825894"));
builder.Services.AddTransient<SolicitudManager>();
builder.Services.AddTransient<CiudadanoManager>();
builder.Services.AddTransient<FuncionarioManager>();
builder.Services.AddTransient<GestionManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();