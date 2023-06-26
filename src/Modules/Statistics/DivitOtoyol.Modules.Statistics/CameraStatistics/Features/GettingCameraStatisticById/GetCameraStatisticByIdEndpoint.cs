using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.GettingCameraStatisticById;

public class GetCameraStatisticByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{CameraStatisticsConfigs.CameraStatisticsPrefixUri}/{{id}}",
                GetCameraStatisticById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetCameraStatisticByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(CameraStatisticsConfigs.Tag)
            .WithName("GetCameraStatisticById")
            .WithDisplayName("Get Camera Statistic By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Camera Statistic by Id", "Getting Camera Statistic by Id"))
            .WithApiVersionSet(CameraStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetCameraStatisticById(
        long id,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetCameraStatisticById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
