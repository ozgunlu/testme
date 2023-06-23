using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation;

public class CreateLocationEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(LocationsConfigs.LocationsPrefixUri, CreateLocation)
            .AllowAnonymous()
            .Produces<CreateLocationResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(LocationsConfigs.Tag)
            .WithName("CreateLocation")
            .WithDisplayName("Register New Location for Location.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Location", "Creating a New Location"))
            .WithApiVersionSet(LocationsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateLocation(
        CreateLocationRequest request,
        IGatewayProcessor<LocationModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateLocation(request.Name, request.ParentId);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{LocationsConfigs.LocationsPrefixUri}/{result.Location.Id}",
                result);
        });
    }
}
