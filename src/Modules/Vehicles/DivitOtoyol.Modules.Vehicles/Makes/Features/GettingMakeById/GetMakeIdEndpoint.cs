using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.GettingMakeById;

public class GetMakeByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{MakesConfigs.MakesPrefixUri}/{{id}}",
                GetMakeById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetMakeByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(MakesConfigs.Tag)
            .WithName("GetMakeById")
            .WithDisplayName("Get Make By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Make by Id", "Getting Make by Id"))
            .WithApiVersionSet(MakesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetMakeById(
        long id,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetMakeById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
