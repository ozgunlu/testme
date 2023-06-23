using Newtonsoft.Json.Linq;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;

public record RecordDto
{
    public long Id { get; init; }
    public string Plate { get; init; } = default!;
    public long CameraId { get; init; }
    public string CameraName { get; init; }
    public long MakeId { get; init; }
    public string MakeName { get; init; } = default!;
    public long ModelId { get; init; }
    public string ModelName { get; init; } = default!;
    public long ColorId { get; init; }
    public string ColorName { get; init; } = default!;
    public DateTime LprDate { get; init; }
    public string ImagePath { get; init; } = default!;
    public JObject Metadata { get; init; } = new JObject();
}
