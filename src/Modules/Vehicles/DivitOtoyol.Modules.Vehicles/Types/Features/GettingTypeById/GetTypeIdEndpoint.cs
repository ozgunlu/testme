using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.GettingTypeById;

public class GetTypeByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{TypesConfigs.TypesPrefixUri}/{{id}}",
                GetTypeById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetTypeByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(TypesConfigs.Tag)
            .WithName("GetTypeById")
            .WithDisplayName("Get Type By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Type by Id", "Getting Type by Id"))
            .WithApiVersionSet(TypesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetTypeById(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetTypeById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
