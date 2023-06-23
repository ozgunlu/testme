using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord;

// POST api/v1/plate-recognitions/records
public static class CreateRecordEndpoint
{
    internal static IEndpointRouteBuilder MapCreateRecordsEndpoint(this IEndpointRouteBuilder endpoints)
    {        
        endpoints.MapPost($"{RecordsConfigs.RecordsPrefixUri}", CreateRecord)

            // WithOpenApi should placed before versioning and other things
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Creating a New Record", Description = "Creating a New Record"
            })
            .WithTags(RecordsConfigs.Tag)
            //.RequireAuthorization()
            .Produces<CreateRecordResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateRecord")
            .WithDisplayName("Create a new record.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Record", "Creating a New Record"))
            .WithApiVersionSet(RecordsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return endpoints;
    }

    private static Task<IResult> CreateRecord(
        CreateRecordRequest request,
        IGatewayProcessor<PlateRecognitionModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async (commandProcessor, mapper) =>
        {
            var command = mapper.Map<CreateRecord>(request);
            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.CreatedAtRoute("GetRecordById", new {id = result.Record.Id}, result);
        });
    }
}
