using Microsoft.Extensions.Configuration;

namespace BackendApp.Migrator;

public static class MigratorConfiguration
{
    private static readonly IConfiguration Configuration;

    public static string ConnectionString => Configuration["ConnectionStrings:MySql"];

    static MigratorConfiguration()
    {
        Configuration = BuildConfiguration();
    }
    
    private static IConfiguration BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("MIGRATOR_ENV");
        
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.migrator.json")
            .AddJsonFile($"appsettings.migrator.{env}.json", true);

        return builder
            .AddEnvironmentVariables()
            .Build();
    }
}