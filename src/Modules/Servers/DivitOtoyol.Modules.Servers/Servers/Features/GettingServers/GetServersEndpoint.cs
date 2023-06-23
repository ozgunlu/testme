using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Servers.Servers.Features.GettingServers;

// https://www.youtube.com/watch?v=SDu0MA6TmuM
// https://github.com/ardalis/ApiEndpoints
public class GetServersEndpoint : EndpointBaseAsync
    .WithRequest<GetServersRequest?>
    .WithActionResult<GetServersResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetServersEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(ServersConfigs.ServersPrefixUri, Name = "GetServers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all servers",
        Description = "Get all servers",
        OperationId = "GetServers",
        Tags = new[] { ServersConfigs.Tag })]
    public override async Task<ActionResult<GetServersResponse>> HandleAsync(
        [FromQuery] GetServersRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetServers
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
