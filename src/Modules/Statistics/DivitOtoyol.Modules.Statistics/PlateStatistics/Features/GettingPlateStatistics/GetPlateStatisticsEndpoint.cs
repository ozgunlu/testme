using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.GettingPlateStatistics;

public class GetPlateStatisticsEndpoint : EndpointBaseAsync
    .WithRequest<GetPlateStatisticsRequest?>
    .WithActionResult<GetPlateStatisticsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetPlateStatisticsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(PlateStatisticsConfigs.PlateStatisticsPrefixUri, Name = "GetPlateStatistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all plate statistics",
        Description = "Get all plate statistics",
        OperationId = "GetPlateStatistics",
        Tags = new[] { PlateStatisticsConfigs.Tag })]
    public override async Task<ActionResult<GetPlateStatisticsResponse>> HandleAsync(
        [FromQuery] GetPlateStatisticsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetPlateStatistics
            {
                Page = request.Page,
                Sorts = request.Sorts,
                PageSize = request.PageSize,
                Filters = request.Filters,
                Includes = request.Includes,
            },
            cancellationToken);

        return Ok(result);
    }
}
