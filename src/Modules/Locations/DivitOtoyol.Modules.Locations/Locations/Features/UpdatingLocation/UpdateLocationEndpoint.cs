using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Locations.Locations.Features.UpdatingLocation;

public class UpdateLocationEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(LocationsConfigs.LocationsPrefixUri + "/{id}", UpdateLocation)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(LocationsConfigs.Tag)
            .WithName("UpdateLocation")
            .WithDisplayName("Update a location.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Location", "Updating Location"))
            .WithApiVersionSet(LocationsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateLocation(
        long id,
        UpdateLocationRequest request,
        IGatewayProcessor<LocationModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateLocation(id, request.Name, request.ParentId);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
