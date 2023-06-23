using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Locations.Locations.Features.GettingLocationById;

public class GetLocationByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{LocationsConfigs.LocationsPrefixUri}/{{id}}",
                GetLocationById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetLocationByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(LocationsConfigs.Tag)
            .WithName("GetLocationById")
            .WithDisplayName("Get Location By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Location by Id", "Getting Location by Id"))
            .WithApiVersionSet(LocationsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetLocationById(
        long id,
        IGatewayProcessor<LocationModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetLocationById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
