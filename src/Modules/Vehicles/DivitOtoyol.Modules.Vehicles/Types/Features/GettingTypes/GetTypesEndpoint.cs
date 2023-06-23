using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.GettingTypes;

public class GetTypesEndpoint : EndpointBaseAsync
    .WithRequest<GetTypesRequest?>
    .WithActionResult<GetTypesResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetTypesEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(TypesConfigs.TypesPrefixUri, Name = "GetTypes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get all types.",
        Description = "Get all types.",
        OperationId = "GetTypes",
        Tags = new[] { TypesConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetTypesResponse>> HandleAsync(
        [FromQuery] GetTypesRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetTypes
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
