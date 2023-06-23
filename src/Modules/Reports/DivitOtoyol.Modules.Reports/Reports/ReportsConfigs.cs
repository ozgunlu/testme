using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.Persistence;

namespace DivitOtoyol.Modules.Reports.Reports;

internal static class ReportsConfigs
{
    public const string Tag = "Option";
    public const string ReportsPrefixUri =
        $"{ReportModuleConfiguration.ReportModulePrefixUri}/reports";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddReportsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, ReportEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapReportsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
