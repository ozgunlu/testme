using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.DeletingRecord;

public class DeleteRecordEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{RecordsConfigs.RecordsPrefixUri}/{{id}}", DeleteRecord)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(RecordsConfigs.Tag)
            .WithName("DeleteRecord")
            .WithDisplayName("Delete Record for PlateRecognition.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Record", "Delete Record"))
            .WithApiVersionSet(RecordsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteRecord(
        long id,
        IGatewayProcessor<PlateRecognitionModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteRecord(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
