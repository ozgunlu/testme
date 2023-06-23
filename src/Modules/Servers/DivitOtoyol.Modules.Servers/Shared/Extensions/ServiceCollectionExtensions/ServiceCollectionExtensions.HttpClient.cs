using BuildingBlocks.Resiliency.Extensions;
using DivitOtoyol.Modules.Servers.Shared.Location;

namespace DivitOtoyol.Modules.Servers.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<LocationsApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{ServerModuleConfiguration.ModuleName}:{nameof(LocationsApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddHttpApiClient<ILocationApiClient, LocationApiClient>();

        return services;
    }
}
