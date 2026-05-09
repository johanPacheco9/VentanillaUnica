using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VentanillaUnica.Components;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
using VentanillaUnica.Services.Ciudadano;
using VentanillaUnica.Services.Funcionarios;
using VentanillaUnica.Services.Gestion;
using VentanillaUnica.Services.Solicitud;
using VentanillaUnica.Services.TiposTramites;
using VentanillaUnica.Services.Tramites;
using VentanillaUnica.Services.Users; // Asegúrate de tener este namespace

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DE SERVICIOS (CONTENEDOR)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=VentanillaUnica;Username=postgres;Password=1098825894"));

builder.Services.AddControllers();
// Servicios de Negocio
builder.Services.AddTransient<SolicitudManager>();
builder.Services.AddTransient<CiudadanoManager>();
builder.Services.AddTransient<FuncionarioManager>();
builder.Services.AddTransient<GestionManager>();
builder.Services.AddTransient<TramitesManager>();
builder.Services.AddTransient<TiposTramitesManager>();
builder.Services.AddTransient<UserManager>(); // <-- ¡No olvides registrar tu UserManager!

// Seguridad y Password Hashing
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// CONFIGURACIÓN DE AUTENTICACIÓN (DEBE IR ANTES DEL BUILD)
string secretKey = "V3nt4n1ll4_Un1c4_S3cr3t_K3y_2026_!#";
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "VentanillaAuth";
        options.LoginPath = "/";
        options.AccessDeniedPath = "/404";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/404";
});

// 2. CONSTRUCCIÓN DE LA APP (PUNTO CRÍTICO)
var app = builder.Build();

// 3. CONFIGURACIÓN DEL PIPELINE (MIDDLEWARE)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Manejo de 404
app.UseStatusCodePagesWithReExecute("/404"); // Ajustado a la ruta que definimos

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Middlewares de Seguridad
app.UseAuthentication(); // ¿Quién es?
app.UseAuthorization();  // ¿Qué puede hacer?

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();