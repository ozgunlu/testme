using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Domain;
using DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Models;

public class CameraStatistic : Aggregate<CameraStatisticId>
{
    public LocationInformation LocationInformation { get; private set; } = default!;
    public CameraInformation CameraInformation { get; private set; } = default!;
    public TypeInformation TypeInformation { get; private set; } = default!;
    public MakeInformation MakeInformation { get; private set; } = default!;
    public ModelInformation ModelInformation { get; private set; } = default!;
    public ColorInformation ColorInformation { get; private set; } = default!;
    public string Plate { get; private set; } = default!;
    public long TotalPassages { get; private set; } = default!;
    public DateTime FirstSeenAt { get; private set; } = default!;
    public DateTime LastSeenAt { get; private set; } = default!;

    public static CameraStatistic Create(
        CameraStatisticId id,
        LocationInformation locationInformation,
        CameraInformation cameraInformation,
        TypeInformation typeInformation,
        MakeInformation makeInformation,
        ModelInformation modelInformation,
        ColorInformation colorInformation,
        string plate,
        DateTime lprDate)
    {
        Guard.Against.Null(locationInformation, new CameraStatisticDomainException("LocationInformation cannot be null"));
        Guard.Against.Null(cameraInformation, new CameraStatisticDomainException("CameraInformation cannot be null"));
        Guard.Against.Null(typeInformation, new CameraStatisticDomainException("TypeInformation cannot be null"));
        Guard.Against.Null(makeInformation, new CameraStatisticDomainException("MakeInformation cannot be null"));
        Guard.Against.Null(modelInformation, new CameraStatisticDomainException("ModelInformation cannot be null"));
        Guard.Against.Null(colorInformation, new CameraStatisticDomainException("ColorInformation cannot be null"));

        var cameraStatistic = new CameraStatistic
        {
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Plate Statistic id can not be null")),
            LocationInformation = locationInformation,
            CameraInformation = cameraInformation,
            TypeInformation = typeInformation,
            MakeInformation = makeInformation,
            ModelInformation = modelInformation,
            ColorInformation = colorInformation,
        };

        cameraStatistic.ChangePlate(plate);
        cameraStatistic.InitializePassages(lprDate);

        cameraStatistic.AddDomainEvents(new CameraStatisticCreated(cameraStatistic));

        return cameraStatistic;
    }

    /// <summary>
    /// Sets plate statistic item plate.
    /// </summary>
    /// <param name="plate">The plate to be changed.</param>
    public void ChangePlate(string plate)
    {
        Plate = plate;
    }

    /// <summary>
    /// Updates the plate statistics' location information.
    /// </summary>
    /// <param name="locationInformation">The new location information to be updated.</param>
    public void ChangeLocationInformation(LocationInformation locationInformation)
    {
        Guard.Against.Null(locationInformation, new CameraStatisticDomainException("LocationInformation cannot be null"));

        LocationInformation = locationInformation;
    }

    /// <summary>
    /// Updates the plate statistics' camera information.
    /// </summary>
    /// <param name="cameraInformation">The new camera information to be updated.</param>
    public void ChangeCameraInformation(CameraInformation cameraInformation)
    {
        Guard.Against.Null(cameraInformation, new CameraStatisticDomainException("CameraInformation cannot be null"));

        CameraInformation = cameraInformation;
    }

    /// <summary>
    /// Updates the plate statistics' type information.
    /// </summary>
    /// <param name="typeInformation">The new type information to be updated.</param>
    public void ChangeTypeInformation(TypeInformation typeInformation)
    {
        Guard.Against.Null(typeInformation, new CameraStatisticDomainException("TypeInformation cannot be null"));

        TypeInformation = typeInformation;
    }

    /// <summary>
    /// Updates the plate statistics' make information.
    /// </summary>
    /// <param name="makeInformation">The new make information to be updated.</param>
    public void ChangeMakeInformation(MakeInformation makeInformation)
    {
        Guard.Against.Null(makeInformation, new CameraStatisticDomainException("MakeInformation cannot be null"));

        MakeInformation = makeInformation;
    }

    /// <summary>
    /// Updates the plate statistics' model information.
    /// </summary>
    /// <param name="modelInformation">The new model information to be updated.</param>
    public void ChangeModelInformation(ModelInformation modelInformation)
    {
        Guard.Against.Null(modelInformation, new CameraStatisticDomainException("ModelInformation cannot be null"));

        ModelInformation = modelInformation;
    }

    /// <summary>
    /// Updates the plate statistics' color information.
    /// </summary>
    /// <param name="colorInformation">The new color information to be updated.</param>
    public void ChangeColorInformation(ColorInformation colorInformation)
    {
        Guard.Against.Null(colorInformation, new CameraStatisticDomainException("ColorInformation cannot be null"));

        ColorInformation = colorInformation;
    }

    public void InitializePassages(DateTime lprDate)
    {
        TotalPassages = 1;
        FirstSeenAt = lprDate;
        LastSeenAt = lprDate;
    }

    public void UpdatePassages(DateTime lprDate)
    {
        TotalPassages++;
        LastSeenAt = lprDate;
    }
}
