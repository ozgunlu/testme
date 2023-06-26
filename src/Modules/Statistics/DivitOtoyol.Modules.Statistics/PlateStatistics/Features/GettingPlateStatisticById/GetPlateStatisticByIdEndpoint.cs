using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.GettingPlateStatisticById;

public class GetPlateStatisticByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{PlateStatisticsConfigs.PlateStatisticsPrefixUri}/{{id}}",
                GetPlateStatisticById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetPlateStatisticByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(PlateStatisticsConfigs.Tag)
            .WithName("GetPlateStatisticById")
            .WithDisplayName("Get Plate Statistic By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Plate Statistic by Id", "Getting Plate Statistic by Id"))
            .WithApiVersionSet(PlateStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetPlateStatisticById(
        long id,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetPlateStatisticById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
