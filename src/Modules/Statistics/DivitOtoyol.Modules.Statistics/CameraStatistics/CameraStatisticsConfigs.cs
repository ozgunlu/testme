using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics;

internal static class CameraStatisticsConfigs
{
    public const string Tag = "CameraStatistic";
    public const string CameraStatisticsPrefixUri =
        $"{StatisticModuleConfiguration.StatisticModulePrefixUri}/camera-statistics";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddCameraStatisticsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, CameraStatisticEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapCameraStatisticsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
