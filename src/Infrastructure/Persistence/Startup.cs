using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Common;
using FSH.WebApi.Infrastructure.Persistence.ConnectionString;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using FSH.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FSH.WebApi.Infrastructure.Persistence;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        // TODO: there must be a cleaner way to do IOptions validation...
        var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        string? rootConnectionString = databaseSettings.ConnectionString;
        if (string.IsNullOrEmpty(rootConnectionString))
        {
            throw new InvalidOperationException("DB ConnectionString is not configured.");
        }

        string? dbProvider = databaseSettings.DBProvider;
        if (string.IsNullOrEmpty(dbProvider))
        {
            throw new InvalidOperationException("DB Provider is not configured.");
        }

        _logger.Information($"Current DB Provider : {dbProvider}");

        return services
            .Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)))

            .AddDbContext<ApplicationDbContext>(m => m.UseDatabase(dbProvider, rootConnectionString))
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
            .AddTransient<CustomSeederRunner>()
            .AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>())

            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>();

    }

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        switch (dbProvider.ToLowerInvariant())
        {
            case DbProviderKeys.SqlServer:
                return builder.UseSqlServer(connectionString, e =>
                     e.MigrationsAssembly("Migrators.MSSQL"));

            default:
                throw new InvalidOperationException($"DB Provider {dbProvider} is not supported.");
        }
    }

}