using BuildingBlocks.Abstractions.Web.Module;
using BuildingBlocks.Core;
using BuildingBlocks.Core.Extensions;
using BuildingBlocks.Core.Messaging.Extensions;
using DivitOtoyol.Modules.Reports.Reports;
using DivitOtoyol.Modules.Reports.Shared.Extensions.ApplicationBuilderExtensions;
using DivitOtoyol.Modules.Reports.Shared.Extensions.ServiceCollectionExtensions;

namespace DivitOtoyol.Modules.Reports;

public class ReportModuleConfiguration : IModuleDefinition
{
    public const string ReportModulePrefixUri = "api/v{version:apiVersion}/reports";
    public const string ModuleName = "Reports";

    public void AddModuleServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddInfrastructure(configuration);
        services.AddStorage(configuration);

        // Add Sub Modules Services
        services.AddReportsServices();
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

        app.SubscribeAllMessageFromAssemblyOfType<ReportRoot>();

        app.UseInfrastructure();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Add Sub Modules Endpoints
        endpoints.MapReportsEndpoints();

        endpoints.MapGet("reports", (HttpContext context) =>
        {
            var requestId = context.Request.Headers.TryGetValue("X-Request-Id", out var requestIdHeader)
                ? requestIdHeader.FirstOrDefault()
                : string.Empty;

            return $"Reports Service Apis, RequestId: {requestId}";
        }).ExcludeFromDescription();
    }
}
