using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.DeletingColor;

public class DeleteColorEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{ColorsConfigs.ColorsPrefixUri}/{{id}}", DeleteColor)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ColorsConfigs.Tag)
            .WithName("DeleteColor")
            .WithDisplayName("Delete Color for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Color", "Delete Color"))
            .WithApiVersionSet(ColorsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteColor(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteColor(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
