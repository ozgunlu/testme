using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.GettingLocationStatistics;

public record GetLocationStatistics : ListQuery<GetLocationStatisticsResponse>;

public class GetLocationStatisticsValidator : AbstractValidator<GetLocationStatistics>
{
    public GetLocationStatisticsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetLocationStatisticsHandler : IQueryHandler<GetLocationStatistics, GetLocationStatisticsResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetLocationStatisticsHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetLocationStatisticsResponse> Handle(GetLocationStatistics request, CancellationToken cancellationToken)
    {
        var locationStatistics = await _statisticDbContext.LocationStatistics
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<LocationStatistic, LocationStatisticDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetLocationStatisticsResponse(locationStatistics);
    }
}
