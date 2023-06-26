using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic;

public class CreateCameraStatisticEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CameraStatisticsConfigs.CameraStatisticsPrefixUri, CreateCameraStatistic)
            .AllowAnonymous()
            .Produces<CreateCameraStatisticResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(CameraStatisticsConfigs.Tag)
            .WithName("CreateCameraStatistic")
            .WithDisplayName("Create New CameraStatistic for Statistics.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New CameraStatistic", "Creating a New CameraStatistic"))
            .WithApiVersionSet(CameraStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreateCameraStatistic(
        CreateCameraStatisticRequest request,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreateCameraStatistic(
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
                $"{CameraStatisticsConfigs.CameraStatisticsPrefixUri}/{result.CameraStatistic.Id}",
                result);
        });
    }
}
