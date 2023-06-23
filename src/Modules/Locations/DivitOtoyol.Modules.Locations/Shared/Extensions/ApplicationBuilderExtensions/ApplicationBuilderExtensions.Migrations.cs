using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Locations.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{LocationModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var locationDbContext = serviceScope.ServiceProvider.GetRequiredService<LocationDbContext>();

            logger.LogInformation("Updating location database...");

            await locationDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated location database");
        }
    }
}
