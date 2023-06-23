using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption;

public class CreateOptionEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(OptionsConfigs.OptionsPrefixUri, CreateOption)
            .AllowAnonymous()
            .Produces<CreateOptionResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(OptionsConfigs.Tag)
            .WithName("CreateOption")
            .WithDisplayName("Register New Option for System.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Option", "Creating a New Option"))
            .WithApiVersionSet(OptionsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateOption(
        CreateOptionRequest request,
        IGatewayProcessor<SystemModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateOption(request.Key, request.Value, request.Modules, request.Description, request.CanUpdate, request.CanDelete);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{OptionsConfigs.OptionsPrefixUri}/{result.Option.Id}",
                result);
        });
    }
}
