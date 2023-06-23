using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.UpdatingColor;

public class UpdateColorEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost($"{ColorsConfigs.ColorsPrefixUri}/{{id}}", UpdateColor)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags(ColorsConfigs.Tag)
            .WithName("UpdateColor")
            .WithDisplayName("Update a color.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Color", "Updating Color"))
            .WithApiVersionSet(ColorsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateColor(
        long id,
        UpdateColorRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateColor(id, request.Name);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
