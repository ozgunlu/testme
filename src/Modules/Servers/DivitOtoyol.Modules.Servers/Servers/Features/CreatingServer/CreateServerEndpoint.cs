using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer;

public class CreateServerEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ServersConfigs.ServersPrefixUri, CreateServer)
            .AllowAnonymous()
            .Produces<CreateServerResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ServersConfigs.Tag)
            .WithName("CreateServer")
            .WithDisplayName("Create New Server for Server.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Server", "Creating a New Server"))
            .WithApiVersionSet(ServersConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateServer(
        CreateServerRequest request,
        IGatewayProcessor<ServerModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateServer(request.LocationId, request.Name, request.Ip);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{ServersConfigs.ServersPrefixUri}/{result.Server.Id}",
                result);
        });
    }
}
