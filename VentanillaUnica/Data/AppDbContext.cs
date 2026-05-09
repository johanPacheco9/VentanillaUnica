using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Models;
namespace VentanillaUnica.Data;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // DbSets
    public DbSet<Ciudadano> Ciudadanos { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Funcionario> Funcionario { get; set; }
    public DbSet<GestionSolicitud> GestionesSolicitud { get; set; }
    public DbSet<TipoTramite> TipoTramites { get; set; }
    
    public DbSet<User> Users { get; set; }
}


