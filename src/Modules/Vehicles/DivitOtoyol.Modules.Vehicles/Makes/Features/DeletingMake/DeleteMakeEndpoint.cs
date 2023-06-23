using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.DeletingMake;

public class DeleteMakeEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{MakesConfigs.MakesPrefixUri}/{{id}}", DeleteMake)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(MakesConfigs.Tag)
            .WithName("DeleteMake")
            .WithDisplayName("Delete Make for Customer.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Make", "Delete Make"))
            .WithApiVersionSet(MakesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteMake(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteMake(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
