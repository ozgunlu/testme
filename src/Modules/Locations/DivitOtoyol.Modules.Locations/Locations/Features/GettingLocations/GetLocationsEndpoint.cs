using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Locations.Locations.Features.GettingLocations;

public class GetLocationsEndpoint : EndpointBaseAsync
    .WithRequest<GetLocationsRequest?>
    .WithActionResult<GetLocationsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetLocationsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(LocationsConfigs.LocationsPrefixUri, Name = "GetLocations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get all locations",
        Description = "Get all locations",
        OperationId = "GetLocations",
        Tags = new[] { LocationsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetLocationsResponse>> HandleAsync(
        [FromQuery] GetLocationsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetLocations
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
