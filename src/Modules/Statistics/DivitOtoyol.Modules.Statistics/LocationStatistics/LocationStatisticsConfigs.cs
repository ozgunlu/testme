using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics;

internal static class LocationStatisticsConfigs
{
    public const string Tag = "LocationStatistic";
    public const string LocationStatisticsPrefixUri = $"{StatisticModuleConfiguration.StatisticModulePrefixUri}/location-statistics";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddLocationStatisticsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, LocationStatisticEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapLocationStatisticsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();

        return endpoints;
    }
}
