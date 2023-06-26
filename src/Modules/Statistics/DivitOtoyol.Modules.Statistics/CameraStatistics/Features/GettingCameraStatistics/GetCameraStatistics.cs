using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.GettingCameraStatistics;

public record GetCameraStatistics : ListQuery<GetCameraStatisticsResponse>;

public class GetCameraStatisticsValidator : AbstractValidator<GetCameraStatistics>
{
    public GetCameraStatisticsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetCameraStatisticsHandler : IQueryHandler<GetCameraStatistics, GetCameraStatisticsResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetCameraStatisticsHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetCameraStatisticsResponse> Handle(GetCameraStatistics request, CancellationToken cancellationToken)
    {
        var plateStatistics = await _statisticDbContext.CameraStatistics
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<CameraStatistic, CameraStatisticDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetCameraStatisticsResponse(plateStatistics);
    }
}
