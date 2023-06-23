using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.GettingModels;

public class GetModelsEndpoint : EndpointBaseAsync
    .WithRequest<GetModelsRequest?>
    .WithActionResult<GetModelsResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetModelsEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(ModelsConfigs.ModelsPrefixUri, Name = "GetModels")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get Models.",
        Description = "Get Models.",
        OperationId = "GetModels",
        Tags = new[] { ModelsConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetModelsResponse>> HandleAsync(
        [FromQuery] GetModelsRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));

        var result = await _queryProcessor.SendAsync(
            new GetModels
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
