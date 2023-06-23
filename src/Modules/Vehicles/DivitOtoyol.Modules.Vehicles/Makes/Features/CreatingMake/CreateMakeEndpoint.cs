using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake;

public class CreateMakeEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(MakesConfigs.MakesPrefixUri, CreateMake)
            .AllowAnonymous()
            .Produces<CreateMakeResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(MakesConfigs.Tag)
            .WithName("CreateMake")
            .WithDisplayName("Register New Make for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Make", "Creating a New Make"))
            .WithApiVersionSet(MakesConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateMake(
        CreateMakeRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateMake(request.Name);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{MakesConfigs.MakesPrefixUri}/{result.Make.Id}",
                result);
        });
    }
}
