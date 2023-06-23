using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.UpdatingRecord;

public class UpdateLocationEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(RecordsConfigs.RecordsPrefixUri + "/{id}", UpdateRecord)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(RecordsConfigs.Tag)
            .WithName("UpdateRecord")
            .WithDisplayName("Update a record.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Record", "Updating Record"))
            .WithApiVersionSet(RecordsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateRecord(
        long id,
        UpdateRecordRequest request,
        IGatewayProcessor<PlateRecognitionModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateRecord(id, request.Name);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
