using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{CameraModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var cameraDbContext = serviceScope.ServiceProvider.GetRequiredService<CameraDbContext>();

            logger.LogInformation("Updating camera database...");

            await cameraDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated camera database");
        }
    }
}
