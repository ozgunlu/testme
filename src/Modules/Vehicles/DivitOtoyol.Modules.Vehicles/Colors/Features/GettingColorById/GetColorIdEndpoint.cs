using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.GettingColorById;

public class GetColorByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{ColorsConfigs.ColorsPrefixUri}/{{id}}",
                GetColorById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetColorByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ColorsConfigs.Tag)
            .WithName("GetColorById")
            .WithDisplayName("Get Color By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Color by Id", "Getting Color by Id"))
            .WithApiVersionSet(ColorsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetColorById(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetColorById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
