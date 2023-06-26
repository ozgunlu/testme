using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.GettingLocationStatisticById;

public class GetLocationStatisticByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{LocationStatisticsConfigs.LocationStatisticsPrefixUri}/{{id}}",
                GetLocationStatisticById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetLocationStatisticByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(LocationStatisticsConfigs.Tag)
            .WithName("GetLocationStatisticById")
            .WithDisplayName("Get Location Statistic By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Location Statistic by Id", "Getting Location Statistic by Id"))
            .WithApiVersionSet(LocationStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetLocationStatisticById(
        long id,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetLocationStatisticById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
