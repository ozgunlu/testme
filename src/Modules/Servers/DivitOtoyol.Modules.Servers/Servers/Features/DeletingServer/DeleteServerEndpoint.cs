using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Servers.Servers.Features.DeletingServer;

public class DeleteServerEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete($"{ServersConfigs.ServersPrefixUri}/{{id}}", DeleteServer)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ServersConfigs.Tag)
            .WithName("DeleteServer")
            .WithDisplayName("Delete Server.")
            .WithMetadata(new SwaggerOperationAttribute("Delete Server", "Delete Server"))
            .WithApiVersionSet(ServersConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> DeleteServer(
        long id,
        IGatewayProcessor<ServerModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new DeleteServer(id);

            await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
