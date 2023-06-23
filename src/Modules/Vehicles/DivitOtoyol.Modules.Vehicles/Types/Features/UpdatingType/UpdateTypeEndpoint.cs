using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.UpdatingType;

public class UpdateTypeEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(TypesConfigs.TypesPrefixUri + "/{id}", UpdateType)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(TypesConfigs.Tag)
            .WithName("UpdateType")
            .WithDisplayName("Update a type.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Type", "Updating Type"))
            .WithApiVersionSet(TypesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateType(
        long id,
        UpdateTypeRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateType(id, request.Name, request.ParentId);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
