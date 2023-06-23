using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.DeletingModel;

public class DeleteModelEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{ModelsConfigs.ModelsPrefixUri}/{{id}}", DeleteModel)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ModelsConfigs.Tag)
            .WithName("DeleteModel")
            .WithDisplayName("Delete Model for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Model", "Delete Model"))
            .WithApiVersionSet(ModelsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteModel(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteModel(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
