using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptionById;

public class GetOptionByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{OptionsConfigs.OptionsPrefixUri}/{{id}}",
                GetOptionById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetOptionByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OptionsConfigs.Tag)
            .WithName("GetOptionById")
            .WithDisplayName("Get Option By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Option by Id", "Getting Option by Id"))
            .WithApiVersionSet(OptionsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetOptionById(
        long id,
        IGatewayProcessor<SystemModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetOptionById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
