using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.DeletingOption;

public class DeleteOptionEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{OptionsConfigs.OptionsPrefixUri}/{{id}}", DeleteOption)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(OptionsConfigs.Tag)
            .WithName("DeleteOption")
            .WithDisplayName("Delete Option for System.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Option", "Delete Option"))
            .WithApiVersionSet(OptionsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteOption(
        long id,
        IGatewayProcessor<SystemModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteOption(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
