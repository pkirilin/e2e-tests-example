using BackendApp.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace BackendApp.Migrator;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly ILoggerFactory _loggerFactory;

    public AppDbContextFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(GetConnectionString(args), new MySqlServerVersion("8.0.32"))
            .UseLoggerFactory(_loggerFactory)
            .Options;

        return new AppDbContext(options);
    }

    private static string GetConnectionString(IReadOnlyList<string> args)
    {
        if (args.Count > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            return args[0];
        }
        
        return MigratorConfiguration.ConnectionString;
    }
}