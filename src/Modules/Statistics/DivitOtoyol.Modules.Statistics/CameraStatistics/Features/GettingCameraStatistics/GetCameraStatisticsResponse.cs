using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Dtos;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.GettingCameraStatistics;

public record GetCameraStatisticsResponse(ListResultModel<CameraStatisticDto> CameraStatistics);
