using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.Locations.Locations;
using DivitOtoyol.Modules.Locations.Locations.GrpcServices;
using DivitOtoyol.Modules.Locations.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Locations.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.Locations;

public class LocationModuleConfiguration : IModuleDefinition
{
    public const string LocationModulePrefixUri = "api/v{version:apiVersion}/locations";
    public const string ModuleName = "Locations";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddLocationsServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<LocationRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
        await app.SeedData(logger, environment);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapGrpcService<ILocationService>();

        endpoints.MapLocationsEndpoints();

        endpoints.MapGet("locations", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Locations Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
