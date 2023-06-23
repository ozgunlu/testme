namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera;

public record CreateCameraRequest(long LocationId, string Name, string? BiosName, string? SerialNumber, string? Ip);
