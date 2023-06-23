using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.Servers.Servers;
using DivitOtoyol.Modules.Servers.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Servers.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.Servers;

public class ServerModuleConfiguration : IModuleDefinition
{
    public const string ServerModulePrefixUri = "api/v{version:apiVersion}/servers";
    public const string ModuleName = "Servers";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddServersServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<ServerRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
        await app.SeedData(logger, environment);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapServersEndpoints();

        endpoints.MapGet("servers", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Servers Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
