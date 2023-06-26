using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Statistics.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigrations(this IApplicationBuilder app, ILogger logger)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue<bool>(
                $"{StatisticModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var statisticDbContext = serviceScope.ServiceProvider.GetRequiredService<StatisticDbContext>();

            logger.LogInformation("Updating statistic database...");

            await statisticDbContext.Database.MigrateAsync();

            logger.LogInformation("Updated statistic database");
        }
    }
}
