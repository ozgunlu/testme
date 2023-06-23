using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Makes.Data;

namespace DivitOtoyol.Modules.Vehicles.Makes;

internal static class MakesConfigs
{
    public const string Tag = "Vehicle Makes";
    public const string MakesPrefixUri =
        $"{VehicleModuleConfiguration.VehicleModulePrefixUri}/makes";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddMakeServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, MakeDataSeeder>();
        services.AddSingleton<IEventMapper, MakeEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapMakesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
