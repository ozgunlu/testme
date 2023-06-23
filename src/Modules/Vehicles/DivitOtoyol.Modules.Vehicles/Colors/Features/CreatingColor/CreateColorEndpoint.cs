using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor;

public class CreateColorEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ColorsConfigs.ColorsPrefixUri, CreateColor)
            .AllowAnonymous()
            .Produces<CreateColorResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ColorsConfigs.Tag)
            .WithName("CreateColor")
            .WithDisplayName("Create New Color for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Color", "Creating a New Color"))
            .WithApiVersionSet(ColorsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateColor(
        CreateColorRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateColor(request.Name);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{ColorsConfigs.ColorsPrefixUri}/{result.Color.Id}",
                result);
        });
    }
}
