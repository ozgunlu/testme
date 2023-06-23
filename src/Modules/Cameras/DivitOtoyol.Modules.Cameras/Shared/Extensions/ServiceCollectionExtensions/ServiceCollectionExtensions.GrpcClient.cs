using DivitOtoyol.Modules.Cameras.Shared.Location;
using DivitOtoyol.Modules.Locations.Locations.Protos;

namespace DivitOtoyol.Modules.Cameras.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomGrpcClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<LocationsGrpcClientOptions>().Bind(
                configuration.GetSection(
                    $"{CameraModuleConfiguration.ModuleName}:{nameof(LocationsGrpcClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddGrpcClient<LocationService.LocationServiceClient>(o =>
        {
            o.Address = new Uri(configuration["Cameras:LocationsGrpcClientOptions:BaseGrpcAddress"]);
        });

        services.AddScoped<ILocationGrpcClient, LocationGrpcClient>();

        return services;
    }
}
