using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptions;

public class GetOptionsEndpoint : EndpointBaseAsync
    .WithRequest<GetOptionsRequest?>
    .WithActionResult<GetOptionsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetOptionsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(OptionsConfigs.OptionsPrefixUri, Name = "GetOptions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get all options",
        Description = "Get all options",
        OperationId = "GetOptions",
        Tags = new[] { OptionsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetOptionsResponse>> HandleAsync(
        [FromQuery] GetOptionsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetOptions
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
