using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecords;

// https://www.youtube.com/watch?v=SDu0MA6TmuM
// https://github.com/ardalis/ApiEndpoints
public class GetRecordsEndpoint : EndpointBaseAsync
    .WithRequest<GetRecordsRequest?>
    .WithActionResult<GetRecordsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetRecordsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(RecordsConfigs.RecordsPrefixUri, Name = "GetRecords")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all records",
        Description = "Get all records",
        OperationId = "GetRecords",
        Tags = new[] { RecordsConfigs.Tag })]
    public override async Task<ActionResult<GetRecordsResponse>> HandleAsync(
        [FromQuery] GetRecordsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetRecords
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
