using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Systems.Options.Data;

namespace DivitOtoyol.Modules.Systems.Options;

internal static class OptionsConfigs
{
    public const string Tag = "Option";
    public const string OptionsPrefixUri =
        $"{SystemModuleConfiguration.SystemModulePrefixUri}/options";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddOptionsServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, OptionDataSeeder>();
        services.AddSingleton<IEventMapper, OptionEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapOptionsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
