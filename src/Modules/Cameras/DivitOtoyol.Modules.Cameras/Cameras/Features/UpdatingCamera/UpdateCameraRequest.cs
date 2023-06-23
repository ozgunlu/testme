namespace DivitOtoyol.Modules.Cameras.Cameras.Features.UpdatingCamera;

public record UpdateCameraRequest
{
    public long LocationId { get; init; }
    public string Name { get; init; }
    public string? BiosName { get; init; }
    public string? SerialNumber { get; init; }
    public string? Ip { get; init; }
}
