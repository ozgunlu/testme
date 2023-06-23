using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.GetActiveBaseFolder;

public class GetActiveBaseFolderEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{OptionsConfigs.OptionsPrefixUri}/get-active-base-folder",
                GetActiveBaseFolder)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetActiveBaseFolderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OptionsConfigs.Tag)
            .WithName("GetActiveBaseFolder")
            .WithDisplayName("Get Active Base Folder.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Active Base Folder", "Getting Active Base Folder"))
            .WithApiVersionSet(OptionsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetActiveBaseFolder(
        IGatewayProcessor<SystemModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetActiveBaseFolder(), cancellationToken);

            return Results.Ok(result);
        });
    }
}
