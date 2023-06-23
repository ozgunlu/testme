using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Servers.Servers.Features.GettingServerById;

public class GetServerByIdEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
                $"{ServersConfigs.ServersPrefixUri}/{{id}}",
                GetServerById)
            //.RequireAuthorization(CustomersConstants.Role.Admin)
            .Produces<GetServerByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ServersConfigs.Tag)
            .WithName("GetServerById")
            .WithDisplayName("Get Server By Id.")
            .WithMetadata(new SwaggerOperationAttribute("Getting Server by Id", "Getting Server by Id"))
            .WithApiVersionSet(ServersConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> GetServerById(
        long id,
        IGatewayProcessor<ServerModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(id, nameof(id));

        return gatewayProcessor.ExecuteQuery(async queryProcessor =>
        {
            var result = await queryProcessor.SendAsync(new GetServerById(id), cancellationToken);

            return Results.Ok(result);
        });
    }
}
