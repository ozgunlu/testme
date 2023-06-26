using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Integration;

public record RecordCreated(long Id, string Plate, long CameraId, string CameraName, long? MakeId, string? MakeName, long? ModelId, string? ModelName, long? ColorId, string? ColorName, DateTime LprDate) :
    IntegrationEvent;
