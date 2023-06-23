using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.GettingMakes;

public class GetMakesEndpoint : EndpointBaseAsync
    .WithRequest<GetMakesRequest?>
    .WithActionResult<GetMakesResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetMakesEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(MakesConfigs.MakesPrefixUri, Name = "GetMakes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get Makes.",
        Description = "Get Makes.",
        OperationId = "GetMakes",
        Tags = new[] { MakesConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetMakesResponse>> HandleAsync(
        [FromQuery] GetMakesRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetMakes
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
