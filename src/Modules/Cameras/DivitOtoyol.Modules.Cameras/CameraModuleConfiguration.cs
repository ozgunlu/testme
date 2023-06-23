using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.Cameras.Cameras;
using DivitOtoyol.Modules.Cameras.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Cameras.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.Cameras;

public class CameraModuleConfiguration : IModuleDefinition
{
    public const string CameraModulePrefixUri = "api/v{version:apiVersion}/cameras";
    public const string ModuleName = "Cameras";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddCamerasServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<CameraRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
        await app.SeedData(logger, environment);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapCamerasEndpoints();

        endpoints.MapGet("cameras", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Cameras Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
