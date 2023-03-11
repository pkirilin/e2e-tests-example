using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace BackendApp.Migrator;

internal class Program
{
    internal static async Task<int> Main(string[] args)
    {
        try
        {
            using var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var serviceProvider = BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            var isDbReady = await IsDbReadyAsync(logger, cancellationToken);

            if (!isDbReady)
            {
                return -1;
            }

            await MigrateAsync(args, logger, loggerFactory, cancellationToken);
            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }

        IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
    
            services.AddLogging(logging => logging.AddConsole());

            return services.BuildServiceProvider();
        }
    }

    private static async Task<bool> IsDbReadyAsync(ILogger logger, CancellationToken cancellationToken)
    {
        const int maxRetriesCount = 10;
        var retryNumber = 0;
        var connectionString = MigratorConfiguration.ConnectionString;
        
        logger.LogInformation("Checking if the database is ready...");

        do
        {
            var retryDelayMultiplier = Math.Pow(2, retryNumber) - 1;
            await Task.Delay(TimeSpan.FromSeconds(retryNumber * retryDelayMultiplier), cancellationToken);
            
            try
            {
                retryNumber++;
                await using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                logger.LogInformation("Received error from database, retrying [{RetryNumber}]...", retryNumber);
            }
        } while (retryNumber < maxRetriesCount);

        return false;
    }
    
    private static async Task MigrateAsync(
        string[] args,
        ILogger logger,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var factory = new AppDbContextFactory(loggerFactory);
            var context = factory.CreateDbContext(args);
            await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while applying migrations");
            throw;
        }
    }
}