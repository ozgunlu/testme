using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{VehicleModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var vehicleDbContext = serviceScope.ServiceProvider.GetRequiredService<VehicleDbContext>();

            logger.LogInformation("Updating vehicle database...");

            await vehicleDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated vehicle database");
        }
    }
}
