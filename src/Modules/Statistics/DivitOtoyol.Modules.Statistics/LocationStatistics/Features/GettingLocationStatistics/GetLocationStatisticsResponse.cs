using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Dtos;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.GettingLocationStatistics;

public record GetLocationStatisticsResponse(ListResultModel<LocationStatisticDto> LocationStatistics);
