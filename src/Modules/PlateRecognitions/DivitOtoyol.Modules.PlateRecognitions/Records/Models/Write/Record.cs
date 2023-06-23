using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.DeletingRecord;
using DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;
using Newtonsoft.Json.Linq;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;

// https://event-driven.io/en/notes_about_csharp_records_and_nullable_reference_types/
// https://enterprisecraftsmanship.com/posts/link-to-an-aggregate-reference-or-id/
// https://ardalis.com/avoid-collections-as-properties/?utm_sq=grcpqjyka3
public class Record : Aggregate<RecordId>
{
    public Plate Plate { get; private set; } = default!;
    public CameraInformation CameraInformation { get; private set; } = default!;
    public MakeInformation? MakeInformation { get; private set; } = default!;
    public ModelInformation? ModelInformation { get; private set; } = default!;
    public ColorInformation? ColorInformation { get; private set; } = default!;
    public DateTime LprDate { get; private set; }
    public string ImagePath { get; private set; } = default!;
    public JObject Metadata { get; private set; } = new JObject();

    public static Record Create(
        RecordId id,
        Plate plate,
        CameraInformation cameraInformation,
        MakeInformation makeInformation,
        ModelInformation modelInformation,
        ColorInformation colorInformation,
        DateTime lprDate,
        string imagePath,
        JObject metadata)
    {
        Guard.Against.Null(cameraInformation, new RecordDomainException("CameraInformation cannot be null"));

        var record = new Record
        {
            Id = Guard.Against.Null(id, new RecordDomainException("Record id can not be null"))
        };

        record.ChangePlate(plate);
        record.ChangeCameraInformation(cameraInformation);
        record.ChangeMakeInformation(makeInformation);
        record.ChangeModelInformation(modelInformation);
        record.ChangeColorInformation(colorInformation);
        record.ChangeLprDate(lprDate);
        record.ChangeImagePath(imagePath);
        record.ChangeMetadata(metadata);

        record.AddDomainEvents(new RecordCreated(record));

        return record;
    }

    /// <summary>
    /// Sets vehicles item plate.
    /// </summary>
    /// <param name="plate">The plate to be changed.</param>
    public void ChangePlate(Plate plate)
    {
        Guard.Against.Null(plate, new RecordDomainException("Plate name cannot be null."));

        Plate = plate;
    }

    /// <summary>
    /// Updates the record's camera information.
    /// </summary>
    /// <param name="cameraInformation">The new camera information to be updated.</param>
    public void ChangeCameraInformation(CameraInformation cameraInformation)
    {
        Guard.Against.Null(cameraInformation, new RecordDomainException("CameraInformation cannot be null"));

        CameraInformation = cameraInformation;
    }

    /// <summary>
    /// Updates the record's make information.
    /// </summary>
    /// <param name="makeInformation">The new make information to be updated.</param>
    public void ChangeMakeInformation(MakeInformation? makeInformation)
    {
        MakeInformation = makeInformation;
    }

    /// <summary>
    /// Updates the record's model information.
    /// </summary>
    /// <param name="modelInformation">The new model information to be updated.</param>
    public void ChangeModelInformation(ModelInformation? modelInformation)
    {
        ModelInformation = modelInformation;
    }

    /// <summary>
    /// Updates the record's color information.
    /// </summary>
    /// <param name="colorInformation">The new color information to be updated.</param>
    public void ChangeColorInformation(ColorInformation? colorInformation)
    {
        ColorInformation = colorInformation;
    }

    public void ChangeLprDate(DateTime lprDate)
    {
        Guard.Against.Null(lprDate, new RecordDomainException("LprDate cannot be null."));

        LprDate = lprDate;
    }

    public void ChangeImagePath(string imagePath)
    {
        Guard.Against.NullOrEmpty(imagePath, new RecordDomainException("ImagePath cannot be null or empty."));

        ImagePath = imagePath;
    }

    public void ChangeMetadata(JObject metadata)
    {
        Guard.Against.Null(metadata, new RecordDomainException("Metadata cannot be null."));

        Metadata = metadata;
    }

    public void Delete()
    {
        AddDomainEvents(new RecordDeleted(this));
    }
}
