using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.PlateRecognitions.Records;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.PlateRecognitions;

public class PlateRecognitionModuleConfiguration : IModuleDefinition
{
    public const string PlateRecognitionModulePrefixUri = "api/v{version:apiVersion}/plate-recognitions";
    public const string ModuleName = "PlateRecognitions";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddRecordsServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<PlateRecognitionRoot>();

        app.UseInfrastructure();

        await app.ApplyDatabaseMigrations(logger);
        await app.SeedData(logger, environment);
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapRecordsEndpoints();
        
        endpoints.MapGet("plate-recognitions", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"PlateRecognitions Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
