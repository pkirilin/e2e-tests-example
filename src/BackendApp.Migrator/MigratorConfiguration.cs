using Microsoft.Extensions.Configuration;

namespace BackendApp.Migrator;

public class MigratorConfiguration
{
    private static readonly IConfiguration Configuration;

    public static string ConnectionString => Configuration["ConnectionStrings:MySql"];
    
    static MigratorConfiguration()
    {
        Configuration = BuildConfiguration();
    }
    
    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
        
        var env = Environment.GetEnvironmentVariable("ENV");

        if (env == "Development")
        {
            builder.AddUserSecrets("041ffd97-30fe-4287-9696-0d9dc4be23c1");
        }

        return builder
            .AddEnvironmentVariables()
            .Build();
    }
}