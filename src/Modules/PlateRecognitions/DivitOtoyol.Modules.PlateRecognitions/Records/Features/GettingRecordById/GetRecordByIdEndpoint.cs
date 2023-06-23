using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecordById;

// GET api/v1/platerecognitions/records/{id}
public static class GetRecordByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetRecordByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                $"{RecordsConfigs.RecordsPrefixUri}/{{id}}",
                GetRecordById)
            // .RequireAuthorization()
            .Produces<GetRecordByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(RecordsConfigs.Tag)
            .WithMetadata(new SwaggerOperationAttribute("Getting Record by Id", "Getting Record by Id"))
            .WithName("GetRecordById")
            .WithDisplayName("Get record By Id.")
            .WithApiVersionSet(RecordsConfigs.VersionSet)
            .MapToApiVersion(1.0)
            .HasApiVersion(1.0);

        return endpoints;
    }

    private static Task<IResult> GetRecordById(
        long id,
        IGatewayProcessor<PlateRecognitionModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetRecordById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
