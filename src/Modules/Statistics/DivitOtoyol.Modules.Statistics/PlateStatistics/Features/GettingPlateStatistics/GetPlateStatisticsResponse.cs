using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Dtos;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.GettingPlateStatistics;

public record GetPlateStatisticsResponse(ListResultModel<PlateStatisticDto> PlateStatistics);
