namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Dtos;

public record CameraStatisticDto
{
    public long Id { get; init; }
    public long LocationId { get; init; }
    public string LocationName { get; init; } = default!;
    public long CameraId { get; init; }
    public string CameraName { get; init; } = default!;
    public long TypeId { get; init; }
    public string TypeName { get; init; } = default!;
    public long MakeId { get; init; }
    public string MakeName { get; init; } = default!;
    public long ModelId { get; init; }
    public string ModelName { get; init; } = default!;
    public long ColorId { get; init; }
    public string ColorName { get; init; } = default!;
    public string Plate { get; init; } = default!;
    public long TotalPassages { get; init; }
    public DateTime FirstSeenAt { get; init; }
    public DateTime LastSeenAt { get; init; }
}
