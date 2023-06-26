using BuildingBlocks.Resiliency.Extensions;
using DivitOtoyol.Modules.Cameras.Shared.Location;

namespace DivitOtoyol.Modules.Cameras.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<LocationsApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{CameraModuleConfiguration.ModuleName}:{nameof(LocationsApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddHttpApiClient<ILocationApiClient, LocationApiClient>();

        return services;
    }
}
