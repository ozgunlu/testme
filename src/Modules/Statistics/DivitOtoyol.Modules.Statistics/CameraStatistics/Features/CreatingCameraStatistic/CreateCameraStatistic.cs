using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic;

public record CreateCameraStatistic(
    long LocationId,
    string LocationName,
    long CameraId,
    string CameraName,
    long TypeId,
    string TypeName,
    long MakeId,
    string MakeName,
    long ModelId,
    string ModelName,
    long ColorId,
    string ColorName,
    string Plate,
    DateTime LprDate) : ITxCreateCommand<CreateCameraStatisticResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public class CreateCameraStatisticValidator : AbstractValidator<CreateCameraStatistic>
{
    public CreateCameraStatisticValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Location Id is required.");

        RuleFor(x => x.CameraId)
            .NotEmpty().WithMessage("Camera Id is required.");

        RuleFor(x => x.TypeId)
            .NotEmpty().WithMessage("Type Id is required.");

        RuleFor(x => x.MakeId)
            .NotEmpty().WithMessage("Make Id is required.");

        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model Id is required.");

        RuleFor(x => x.ColorId)
            .NotEmpty().WithMessage("Color Id is required.");

        RuleFor(x => x.Plate)
            .NotEmpty().WithMessage("Plate is required.");

        RuleFor(x => x.LprDate)
            .NotEmpty().WithMessage("LprDate is required.");
    }
}

public class CreateCameraStatisticHandler : ICommandHandler<CreateCameraStatistic, CreateCameraStatisticResponse>
{
    private readonly ILogger<CreateCameraStatisticHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStatisticDbContext _statisticDbContext;

    public CreateCameraStatisticHandler(
        IStatisticDbContext statisticDbContext,
        IMapper mapper,
        ILogger<CreateCameraStatisticHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _statisticDbContext = Guard.Against.Null(statisticDbContext, nameof(statisticDbContext));
    }

    public async Task<CreateCameraStatisticResponse> Handle(
        CreateCameraStatistic command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var locationInformation = LocationInformation.Create(command.LocationId, command.LocationName);
        var cameraInformation = CameraInformation.Create(command.CameraId, command.CameraName);
        var typeInformation = TypeInformation.Create(command.TypeId, command.TypeName);
        var makeInformation = MakeInformation.Create(command.MakeId, command.MakeName);
        var modelInformation = ModelInformation.Create(command.ModelId, command.ModelName);
        var colorInformation = ColorInformation.Create(command.ColorId, command.ColorName);

        var plateStatistic = CameraStatistic.Create(
            command.Id,
            locationInformation,
            cameraInformation,
            typeInformation,
            makeInformation,
            modelInformation,
            colorInformation,
            command.Plate,
            command.LprDate);

        await _statisticDbContext.CameraStatistics.AddAsync(plateStatistic, cancellationToken: cancellationToken);

        await _statisticDbContext.SaveChangesAsync(cancellationToken);

        var created = await _statisticDbContext.CameraStatistics
            .Include(x => x.LocationInformation)
            .Include(x => x.CameraInformation)
            .Include(x => x.TypeInformation)
            .Include(x => x.MakeInformation)
            .Include(x => x.ModelInformation)
            .Include(x => x.ColorInformation)
            .SingleOrDefaultAsync(x => x.Id == plateStatistic.Id, cancellationToken: cancellationToken);

        var cameraStatisticDto = _mapper.Map<CameraStatisticDto>(created);

        _logger.LogInformation("Camera Statistic a with ID: '{CameraStatisticId} created.'", command.Id);

        return new CreateCameraStatisticResponse(cameraStatisticDto);
    }
}
