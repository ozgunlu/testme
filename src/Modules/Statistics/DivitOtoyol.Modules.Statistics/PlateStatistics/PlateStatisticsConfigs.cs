using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics;

internal static class PlateStatisticsConfigs
{
    public const string Tag = "PlateStatistic";
    public const string PlateStatisticsPrefixUri = $"{StatisticModuleConfiguration.StatisticModulePrefixUri}/plate-statistics";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddPlateStatisticsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, PlateStatisticEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapPlateStatisticsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();

        return endpoints;
    }
}
