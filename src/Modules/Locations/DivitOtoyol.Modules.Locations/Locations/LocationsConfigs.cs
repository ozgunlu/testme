using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Locations.Locations.Data;
using DivitOtoyol.Modules.Locations.Locations.GrpcServices;

namespace DivitOtoyol.Modules.Locations.Locations;

public static class LocationsConfigs
{
    public const string Tag = "Location";
    public const string LocationsPrefixUri =
        $"{LocationModuleConfiguration.LocationModulePrefixUri}/locations";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    public static IServiceCollection AddLocationsServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, LocationDataSeeder>();
        services.AddSingleton<IEventMapper, LocationEventMapper>();
        services.AddScoped<LocationServiceImplementation>();
        return services;
    }

    internal static IEndpointRouteBuilder MapLocationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        endpoints.MapGrpcService<LocationServiceImplementation>();
        return endpoints;
    }
}
