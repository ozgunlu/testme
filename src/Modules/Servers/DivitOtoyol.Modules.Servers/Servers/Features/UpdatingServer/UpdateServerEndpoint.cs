using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Servers.Servers.Features.UpdatingServer;

public class UpdateServerEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ServersConfigs.ServersPrefixUri + "/{id}", UpdateServer)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ServersConfigs.Tag)
            .WithName("UpdateServer")
            .WithDisplayName("Update a server.")
            .WithMetadata(new SwaggerOperationAttribute("Updating Server", "Updating Server"))
            .WithApiVersionSet(ServersConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> UpdateServer(
        long id,
        UpdateServerRequest request,
        IGatewayProcessor<ServerModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new UpdateServer(id, request.LocationId, request.Name, request.Ip);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.NoContent();
        });
    }
}
