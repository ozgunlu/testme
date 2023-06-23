using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Models.Data;

namespace DivitOtoyol.Modules.Vehicles.Models;

internal static class ModelsConfigs
{
    public const string Tag = "Vehicle Models";
    public const string ModelsPrefixUri =
        $"{VehicleModuleConfiguration.VehicleModulePrefixUri}/models";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddModelServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ModelDataSeeder>();
        services.AddSingleton<IEventMapper, ModelEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapModelsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
