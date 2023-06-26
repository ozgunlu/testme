using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.GettingLocationStatistics;

public class GetLocationStatisticsEndpoint : EndpointBaseAsync
    .WithRequest<GetLocationStatisticsRequest?>
    .WithActionResult<GetLocationStatisticsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetLocationStatisticsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(LocationStatisticsConfigs.LocationStatisticsPrefixUri, Name = "GetLocationStatistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all location statistics",
        Description = "Get all location statistics",
        OperationId = "GetLocationStatistics",
        Tags = new[] { LocationStatisticsConfigs.Tag })]
    public override async Task<ActionResult<GetLocationStatisticsResponse>> HandleAsync(
        [FromQuery] GetLocationStatisticsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetLocationStatistics
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
