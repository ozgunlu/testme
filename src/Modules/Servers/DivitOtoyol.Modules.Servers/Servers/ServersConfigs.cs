using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Servers.Servers.Data;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer;
using DivitOtoyol.Modules.Servers.Servers.Features.GettingServerById;
using DivitOtoyol.Modules.Servers.Servers.Features.UpdatingServer;

namespace DivitOtoyol.Modules.Servers.Servers;

internal static class ServersConfigs
{
    public const string Tag = "Server";
    public const string ServersPrefixUri = $"{ServerModuleConfiguration.ServerModulePrefixUri}/servers";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddServersServices(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ServerDataSeeder>();
        services.AddSingleton<IEventMapper, ServerEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapServersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();

        return endpoints;
    }
}
