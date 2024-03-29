﻿using BackendApp.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            var context = CreateDbContext(args, loggerFactory);
            await MigrateAsync(context, logger, cancellationToken);
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

    private static AppDbContext CreateDbContext(string[] args, ILoggerFactory loggerFactory)
    {
        var factory = new AppDbContextFactory(loggerFactory);
        var context = factory.CreateDbContext(args);
        return context;
    }
    
    private static async Task MigrateAsync(DbContext context, ILogger logger, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while applying migrations");
            throw;
        }
    }
}