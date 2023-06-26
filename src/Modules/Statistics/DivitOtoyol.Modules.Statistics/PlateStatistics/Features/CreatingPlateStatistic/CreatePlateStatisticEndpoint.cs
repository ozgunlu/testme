using Ardalis.GuardClauses;
using Asp.Versioning.Conventions;
using BuildingBlocks.Abstractions.Web;
using BuildingBlocks.Abstractions.Web.MinimalApi;
using Swashbuckle.AspNetCore.Annotations;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic;

public class CreatePlateStatisticEndpoint : IMinimalEndpointDefinition
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(PlateStatisticsConfigs.PlateStatisticsPrefixUri, CreatePlateStatistic)
            .AllowAnonymous()
            .Produces<CreatePlateStatisticResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(PlateStatisticsConfigs.Tag)
            .WithName("CreatePlateStatistic")
            .WithDisplayName("Create New PlateStatistic for Statistics.")
            .WithMetadata(new SwaggerOperationAttribute("Creating a New PlateStatistic", "Creating a New PlateStatistic"))
            .WithApiVersionSet(PlateStatisticsConfigs.VersionSet)
            .HasApiVersion(1.0);

        return builder;
    }

    private static Task<IResult> CreatePlateStatistic(
        CreatePlateStatisticRequest request,
        IGatewayProcessor<StatisticModuleConfiguration> gatewayProcessor,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        return gatewayProcessor.ExecuteCommand(async commandProcessor =>
        {
            var command = new CreatePlateStatistic(
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
                $"{PlateStatisticsConfigs.PlateStatisticsPrefixUri}/{result.PlateStatistic.Id}",
                result);
        });
    }
}
