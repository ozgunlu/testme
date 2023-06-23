using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.UpdatingMake;

public class UpdateMakeEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost($"{MakesConfigs.MakesPrefixUri}/{{id}}", UpdateMake)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags(MakesConfigs.Tag)
            .WithName("UpdateMake")
            .WithDisplayName("Update a make.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Make", "Updating Make"))
            .WithApiVersionSet(MakesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateMake(
        long id,
        UpdateMakeRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateMake(id, request.Name);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
