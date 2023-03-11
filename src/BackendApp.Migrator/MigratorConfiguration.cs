using Microsoft.Extensions.Configuration;

namespace BackendApp.Migrator;

public static class MigratorConfiguration
{
    private static readonly IConfiguration Configuration;

    public static string ConnectionString => Configuration["ConnectionStrings:MySql"];

    public static bool IsSeedDataEnabled =>
        bool.TryParse(Environment.GetEnvironmentVariable("MIGRATOR_SEED_DATA"), out var seedData) &&
        seedData;
    
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