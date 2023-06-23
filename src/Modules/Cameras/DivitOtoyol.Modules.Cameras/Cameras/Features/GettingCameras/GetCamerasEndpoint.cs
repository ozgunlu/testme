using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Asp.Versioning;
using BuildingBlocks.Abstractions.CQRS.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.GettingCameras;

public class GetCamerasEndpoint : EndpointBaseAsync
    .WithRequest<GetCamerasRequest?>
    .WithActionResult<GetCamerasResponse>
{
    private readonly IQueryProcessor _queryProcessor;

    public GetCamerasEndpoint(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet(CamerasConfigs.CamerasPrefixUri, Name = "GetCameras")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiVersion(1.0)]
    [SwaggerOperation(
        Summary = "Get all cameras",
        Description = "Get all cameras",
        OperationId = "GetCameras",
        Tags = new[] { CamerasConfigs.Tag })]
    //[Authorize(Roles = CustomersConstants.Role.Admin)]
    public override async Task<ActionResult<GetCamerasResponse>> HandleAsync(
        [FromQuery] GetCamerasRequest? request,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(request));


        var result = await _queryProcessor.SendAsync(
            new GetCameras
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
