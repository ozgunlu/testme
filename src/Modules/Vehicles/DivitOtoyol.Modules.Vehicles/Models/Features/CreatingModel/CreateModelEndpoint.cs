using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel;

public class CreateModelEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ModelsConfigs.ModelsPrefixUri, CreateModel)
            .AllowAnonymous()
            .Produces<CreateModelResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(ModelsConfigs.Tag)
            .WithName("CreateModel")
            .WithDisplayName("Register New Model for Vehicle.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New Model", "Creating a New Model"))
            .WithApiVersionSet(ModelsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateModel(
        CreateModelRequest request,
        IGatewayProcessor<VehicleModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateModel(request.Name, request.MakeId, request.TypeId);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{ModelsConfigs.ModelsPrefixUri}/{result.Model.Id}",
                result);
        });
    }
}
