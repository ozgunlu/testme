namespace DivitOtoyol.Modules.Cameras.Cameras.Dtos;

public record CameraDto
{
    public long Id { get; init; }
    public long LocationId { get; init; }
    public string LocationName { get; init; }
    public string Name { get; init; } = default!;
    public string BiosName { get; init; } = default!;
    public string SerialNumber { get; init; } = default!;
    public string Ip { get; init; } = default!;
    public DateTime Created { get; init; }
}
