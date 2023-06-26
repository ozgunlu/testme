using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.GettingPlateStatistics;

public record GetPlateStatistics : ListQuery<GetPlateStatisticsResponse>;

public class GetPlateStatisticsValidator : AbstractValidator<GetPlateStatistics>
{
    public GetPlateStatisticsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetPlateStatisticsHandler : IQueryHandler<GetPlateStatistics, GetPlateStatisticsResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetPlateStatisticsHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetPlateStatisticsResponse> Handle(GetPlateStatistics request, CancellationToken cancellationToken)
    {
        var plateStatistics = await _statisticDbContext.PlateStatistics
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<PlateStatistic, PlateStatisticDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetPlateStatisticsResponse(plateStatistics);
    }
}
