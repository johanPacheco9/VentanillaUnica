using Microsoft.AspNetCore.Identity;
using VentanillaUnica.Data;
using VentanillaUnica.Models;
namespace VentanillaUnica.Services.Users;

public partial class UserManager(AppDbContext dbContext, ILogger<UserManager> logger, IPasswordHasher<User> passwordHasher)
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<UserManager> _logger = logger;
}