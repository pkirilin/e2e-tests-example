using BackendApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Web.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; } = null!;
}