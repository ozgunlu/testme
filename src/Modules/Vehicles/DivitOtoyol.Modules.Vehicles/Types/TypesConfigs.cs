using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Types.Data;

namespace DivitOtoyol.Modules.Vehicles.Types;

internal static class TypesConfigs
{
    public const string Tag = "Vehicle Types";
    public const string TypesPrefixUri =
        $"{VehicleModuleConfiguration.VehicleModulePrefixUri}/types";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddTypeServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, TypeDataSeeder>();
        services.AddSingleton<IEventMapper, TypeEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapTypesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
