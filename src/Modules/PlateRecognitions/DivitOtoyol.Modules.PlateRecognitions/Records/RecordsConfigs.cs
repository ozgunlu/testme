using Asp.Versioning.Builder;
using BuildingBlocks.Abstractions.CQRS.Event;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecordById;

namespace DivitOtoyol.Modules.PlateRecognitions.Records;

internal static class RecordsConfigs
{
    public const string Tag = "Record";
    public const string RecordsPrefixUri = $"{PlateRecognitionModuleConfiguration.PlateRecognitionModulePrefixUri}/records";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddRecordsServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, RecordEventMapper>();

        return services;
    }

    internal static IEndpointRouteBuilder MapRecordsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();

        return endpoints.MapCreateRecordsEndpoint()
            .MapGetRecordByIdEndpoint();
    }
}
