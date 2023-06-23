using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var plateRecognitionDbContext = serviceScope.ServiceProvider.GetRequiredService<PlateRecognitionDbContext>();

            logger.LogInformation("Updating plate recognition database...");

            await plateRecognitionDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated plate recognition database");
        }
    }
}
