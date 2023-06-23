using Asp.Versioning;
using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using BuildingBlocks.Web.Extensions;
using DivitOtoyol.Modules.Vehicles.Colors;
using DivitOtoyol.Modules.Vehicles.Makes;
using DivitOtoyol.Modules.Vehicles.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions.ServiceCollectionExtensions;
using DivitOtoyol.Modules.Vehicles.Types;

namespace DivitOtoyol.Modules.Vehicles;

public class VehicleModuleConfiguration : IModuleDefinition
{
    public const string VehicleModulePrefixUri = "api/v{version:apiVersion}/vehicles";
    public const string ModuleName = "Vehicles";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddTypeServices();
        services.AddMakeServices();
        services.AddModelServices();
        services.AddColorServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<VehicleRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
        await app.SeedData(logger, environment);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapTypesEndpoints();
        endpoints.MapMakesEndpoints();
        endpoints.MapModelsEndpoints();
        endpoints.MapColorsEndpoints();

        endpoints.MapGet("vehicles", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Vehicles Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
