using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.UpdatingCamera;

// PUT api/v1/cameras/cameras/{id}
public class UpdateCameraEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CamerasConfigs.CamerasPrefixUri + "/{id}", UpdateCameras)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags(CamerasConfigs.Tag)
            .WithName("UpdateCamera")
            .WithDisplayName("Update a camera.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Camera", "Updating Camera"))
            .WithApiVersionSet(CamerasConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateCameras(
        long id,
        UpdateCameraRequest request,
        IGatewayProcessor<CameraModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateCamera(
                id,
                request.LocationId,
                request.Name,
                request.BiosName,
                request.SerialNumber,
                request.Ip);

            await commandProcessor.SendAsync(command, cancellationToken);
            return Results.NoContent();
        });
    }
}
