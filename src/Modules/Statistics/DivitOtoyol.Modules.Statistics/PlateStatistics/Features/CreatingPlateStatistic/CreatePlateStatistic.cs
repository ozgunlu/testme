using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic;

public record CreatePlateStatistic(
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
    DateTime LprDate) : ITxCreateCommand<CreatePlateStatisticResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public class CreatePlateStatisticValidator : AbstractValidator<CreatePlateStatistic>
{
    public CreatePlateStatisticValidator()
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

public class CreatePlateStatisticHandler : ICommandHandler<CreatePlateStatistic, CreatePlateStatisticResponse>
{
    private readonly ILogger<CreatePlateStatisticHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStatisticDbContext _statisticDbContext;

    public CreatePlateStatisticHandler(
        IStatisticDbContext statisticDbContext,
        IMapper mapper,
        ILogger<CreatePlateStatisticHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _statisticDbContext = Guard.Against.Null(statisticDbContext, nameof(statisticDbContext));
    }

    public async Task<CreatePlateStatisticResponse> Handle(
        CreatePlateStatistic command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var locationInformation = LocationInformation.Create(command.LocationId, command.LocationName);
        var cameraInformation = CameraInformation.Create(command.CameraId, command.CameraName);
        var typeInformation = TypeInformation.Create(command.TypeId, command.TypeName);
        var makeInformation = MakeInformation.Create(command.MakeId, command.MakeName);
        var modelInformation = ModelInformation.Create(command.ModelId, command.ModelName);
        var colorInformation = ColorInformation.Create(command.ColorId, command.ColorName);

        var plateStatistic = PlateStatistic.Create(
            command.Id,
            locationInformation,
            cameraInformation,
            typeInformation,
            makeInformation,
            modelInformation,
            colorInformation,
            command.Plate,
            command.LprDate);

        await _statisticDbContext.PlateStatistics.AddAsync(plateStatistic, cancellationToken: cancellationToken);

        await _statisticDbContext.SaveChangesAsync(cancellationToken);

        var created = await _statisticDbContext.PlateStatistics
            .Include(x => x.LocationInformation)
            .Include(x => x.CameraInformation)
            .Include(x => x.TypeInformation)
            .Include(x => x.MakeInformation)
            .Include(x => x.ModelInformation)
            .Include(x => x.ColorInformation)
            .SingleOrDefaultAsync(x => x.Id == plateStatistic.Id, cancellationToken: cancellationToken);

        var plateStatisticDto = _mapper.Map<PlateStatisticDto>(created);

        _logger.LogInformation("Plate Statistic a with ID: '{PlateStatisticId} created.'", command.Id);

        return new CreatePlateStatisticResponse(plateStatisticDto);
    }
}
