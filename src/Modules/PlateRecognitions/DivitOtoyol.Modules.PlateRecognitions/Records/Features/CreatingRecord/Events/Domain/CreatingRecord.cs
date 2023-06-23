using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Domain;

public record CreatingRecord(
    RecordId Id,
    Plate Plate,
    CameraInformation CameraInformation,
    MakeInformation MakeInformation,
    ModelInformation ModelInformation,
    ColorInformation ColorInformation) : DomainEvent;
