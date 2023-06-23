using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Systems.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{SystemModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var systemDbContext = serviceScope.ServiceProvider.GetRequiredService<SystemDbContext>();

            logger.LogInformation("Updating system database...");

            await systemDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated system database");
        }
    }
}
