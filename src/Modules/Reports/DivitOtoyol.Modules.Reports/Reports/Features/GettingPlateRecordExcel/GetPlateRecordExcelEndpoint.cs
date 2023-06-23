using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordExcel;

public class GetPlateRecordExcelEndpoint : EndpointBaseAsync
    .WithRequest<GetPlateRecordExcelRequest?>
    .WithActionResult<GetPlateRecordExcelResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetPlateRecordExcelEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(ReportsConfigs.ReportsPrefixUri, Name = "GetPlateRecordExcel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get plate record report excel",
        Description = "Get plate record report excel",
        OperationId = "GetPlateRecordExcel",
        Tags = new[] { ReportsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetPlateRecordExcelResponse>> HandleAsync(
        [FromQuery] GetPlateRecordExcelRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetPlateRecordExcel
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
