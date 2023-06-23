using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Colors.Data;

namespace DivitOtoyol.Modules.Vehicles.Colors;

internal static class ColorsConfigs
{
    public const string Tag = "Vehicle Colors";
    public const string ColorsPrefixUri =
        $"{VehicleModuleConfiguration.VehicleModulePrefixUri}/colors";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddColorServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ColorDataSeeder>();
        services.AddSingleton<IEventMapper, ColorEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapColorsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
