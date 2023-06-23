using BuildingBlocks.Abstractions.Persistence;

namespace DivitOtoyol.Modules.Systems.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task SeedData(this IApplicationBuilder app, ILogger logger, IWebHostEnvironment environment)
    {
        if (!environment.IsEnvironment("test"))
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var seeders = serviceScope.ServiceProvider.GetServices<IDataSeeder>();

            foreach (var seeder in seeders)
            {
                logger.LogInformation("Seeding '{Seed}' started...", seeder.GetType().Name);
                await seeder.SeedAllAsync();
                logger.LogInformation("Seeding '{Seed}' ended...", seeder.GetType().Name);
            }
        }
    }
}
