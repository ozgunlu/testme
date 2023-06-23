using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.GettingColors;

public class GetColorsEndpoint : EndpointBaseAsync
    .WithRequest<GetColorsRequest?>
    .WithActionResult<GetColorsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetColorsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(ColorsConfigs.ColorsPrefixUri, Name = "GetColors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get Colors.",
        Description = "Get Colors.",
        OperationId = "GetColors",
        Tags = new[] { ColorsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetColorsResponse>> HandleAsync(
        [FromQuery] GetColorsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetColors
            {
                Page = request.Page,
                Sorts = request.Sorts,
                PageSize = request.PageSize,
                Filters = request.Filters,
                Includes = request.Includes
            },
            cancellationToken);

        return Ok(result);
    }
}
