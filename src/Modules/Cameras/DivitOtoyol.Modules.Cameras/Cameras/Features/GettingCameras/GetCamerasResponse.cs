using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Cameras.Cameras.Dtos;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.GettingCameras;

public record GetCamerasResponse(ListResultModel<CameraDto> Cameras);
