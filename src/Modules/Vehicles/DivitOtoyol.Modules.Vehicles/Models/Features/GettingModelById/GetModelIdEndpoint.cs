using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.GettingModelById;

public class GetModelByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{ModelsConfigs.ModelsPrefixUri}/{{id}}",
                GetModelById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetModelByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ModelsConfigs.Tag)
            .WithName("GetModelById")
            .WithDisplayName("Get Model By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Model by Id", "Getting Model by Id"))
            .WithApiVersionSet(ModelsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetModelById(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetModelById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
