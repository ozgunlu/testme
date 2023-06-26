using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.Statistics.CameraStatistics;
using DivitOtoyol.Modules.Statistics.LocationStatistics;
using DivitOtoyol.Modules.Statistics.PlateStatistics;
using DivitOtoyol.Modules.Statistics.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Statistics.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.Statistics;

public class StatisticModuleConfiguration : IModuleDefinition
{
    public const string StatisticModulePrefixUri = "api/v{version:apiVersion}/statistics";
    public const string ModuleName = "Statistics";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddCameraStatisticsServices();
        services.AddLocationStatisticsServices();
        services.AddPlateStatisticsServices();
    }

    public async Task ConfigureModule(
        IApplicationBuilder app,
        IConfiguration configuration,
        ILogger logger,
        IWebHostEnvironment environment)
    {
        if (environment.IsEnvironment("test") == false)
        {
            // HostedServices just run on main service provider - It should not await because it will block the main thread.
            await app.ApplicationServices.StartHostedServices();
        }

        ServiceActivator.Configure(app.ApplicationServices);

        app.SubscribeAllMessageFromAssemblyOfType<StatisticRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapCameraStatisticsEndpoints();
        endpoints.MapLocationStatisticsEndpoints();
        endpoints.MapPlateStatisticsEndpoints();

        endpoints.MapGet("statistics", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Statistics Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
