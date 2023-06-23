using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.GettingCameraById;

public class GetCameraByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{CamerasConfigs.CamerasPrefixUri}/{{id}}",
                GetCameraById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetCameraByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(CamerasConfigs.Tag)
            .WithName("GetCameraById")
            .WithDisplayName("Get Camera By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Camera by Id", "Getting Camera by Id"))
            .WithApiVersionSet(CamerasConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetCameraById(
        long id,
        IGatewayProcessor<CameraModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetCameraById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
