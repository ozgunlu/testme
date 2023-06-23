using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera;

public class CreateCameraEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CamerasConfigs.CamerasPrefixUri, CreateCamera)
            .AllowAnonymous()
            .Produces<CreateCameraResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(CamerasConfigs.Tag)
            .WithName("CreateCamera")
            .WithDisplayName("Create New Camera for Camera.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Camera", "Creating a New Camera"))
            .WithApiVersionSet(CamerasConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateCamera(
        CreateCameraRequest request,
        IGatewayProcessor<CameraModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async (commandProcessor, mapper) =>
        {
            var command = new CreateCamera(request.LocationId, request.Name, request.BiosName, request.SerialNumber, request.Ip);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{CamerasConfigs.CamerasPrefixUri}/{result.Camera.Id}",
                result);
        });
    }
}
