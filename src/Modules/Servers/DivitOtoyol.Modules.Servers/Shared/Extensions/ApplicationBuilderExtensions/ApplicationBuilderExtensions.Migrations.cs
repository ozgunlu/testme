using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Servers.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{ServerModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serverDbContext = serviceScope.ServiceProvider.GetRequiredService<ServerDbContext>();

            logger.LogInformation("Updating server database...");

            await serverDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated server database");
        }
    }
}
