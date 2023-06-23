using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptionByKey;

public class GetOptionByKeyEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{OptionsConfigs.OptionsPrefixUri}/key/{{key}}",
                GetOptionByKey)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetOptionByKeyResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OptionsConfigs.Tag)
            .WithName("GetOptionByKey")
            .WithDisplayName("Get Option By Key.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Option by Key", "Getting Option by Key"))
            .WithApiVersionSet(OptionsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetOptionByKey(
        string key,
        IGatewayProcessor<SystemModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(key, nameof(key));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetOptionByKey(key), cancellationToken);

            return Results.Ok(result);
        });
    }
}
