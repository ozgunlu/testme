using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType;

public class CreateTypeEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(TypesConfigs.TypesPrefixUri, CreateType)
            .AllowAnonymous()
            .Produces<CreateTypeResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(TypesConfigs.Tag)
            .WithName("CreateType")
            .WithDisplayName("Create New Type for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Type", "Creating a New Type"))
            .WithApiVersionSet(TypesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateType(
        CreateTypeRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateType(request.Name, request.ParentId);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{TypesConfigs.TypesPrefixUri}/{result.Type.Id}",
                result);
        });
    }
}
