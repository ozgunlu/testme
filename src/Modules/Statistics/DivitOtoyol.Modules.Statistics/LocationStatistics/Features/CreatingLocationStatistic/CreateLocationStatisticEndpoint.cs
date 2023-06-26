using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic;

public class CreateLocationStatisticEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(LocationStatisticsConfigs.LocationStatisticsPrefixUri, CreateLocationStatistic)
            .AllowAnonymous()
            .Produces<CreateLocationStatisticResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(LocationStatisticsConfigs.Tag)
            .WithName("CreateLocationStatistic")
            .WithDisplayName("Create New LocationStatistic for Statistics.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New LocationStatistic", "Creating a New LocationStatistic"))
            .WithApiVersionSet(LocationStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateLocationStatistic(
        CreateLocationStatisticRequest request,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateLocationStatistic(
                request.LocationId,
                request.LocationName,
                request.CameraId,
                request.CameraName,
                request.TypeId,
                request.TypeName,
                request.MakeId,
                request.MakeName,
                request.ModelId,
                request.ModelName,
                request.ColorId,
                request.ColorName,
                request.Plate,
                request.LprDate);

            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.Created(
                $"{LocationStatisticsConfigs.LocationStatisticsPrefixUri}/{result.LocationStatistic.Id}",
                result);
        });
    }
}
