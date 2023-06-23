using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordPdf;

public class GetPlateRecordPdfEndpoint : EndpointBaseAsync
    .WithRequest<GetPlateRecordZipRequest?>
    .WithActionResult<GetPlateRecordZipResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetPlateRecordZipEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(ReportsConfigs.ReportsPrefixUri, Name = "GetPlateRecordZip")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get plate record report zip",
        Description = "Get plate record report zip",
        OperationId = "GetPlateRecordZip",
        Tags = new[] { ReportsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetPlateRecordZipResponse>> HandleAsync(
        [FromQuery] GetPlateRecordZipRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetPlateRecordZip
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
