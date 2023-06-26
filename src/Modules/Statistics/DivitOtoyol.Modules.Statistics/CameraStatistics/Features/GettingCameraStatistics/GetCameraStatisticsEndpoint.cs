using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.GettingCameraStatistics;

public class GetCameraStatisticsEndpoint : EndpointBaseAsync
    .WithRequest<GetCameraStatisticsRequest?>
    .WithActionResult<GetCameraStatisticsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetCameraStatisticsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(CameraStatisticsConfigs.CameraStatisticsPrefixUri, Name = "GetCameraStatistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all camera statistics",
        Description = "Get all camera statistics",
        OperationId = "GetCameraStatistics",
        Tags = new[] { CameraStatisticsConfigs.Tag })]
    public override async Task<ActionResult<GetCameraStatisticsResponse>> HandleAsync(
        [FromQuery] GetCameraStatisticsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetCameraStatistics
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
