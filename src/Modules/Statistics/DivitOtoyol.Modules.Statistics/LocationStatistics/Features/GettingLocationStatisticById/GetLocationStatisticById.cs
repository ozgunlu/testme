using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Application;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using DivitOtoyol.Modules.Statistics.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.GettingLocationStatisticById;

public record GetLocationStatisticById(long Id) : IQuery<GetLocationStatisticByIdResponse>;

internal class GetLocationStatisticByIdValidator : AbstractValidator<GetLocationStatisticById>
{
    public GetLocationStatisticByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetLocationStatisticByIdHandler : IQueryHandler<GetLocationStatisticById, GetLocationStatisticByIdResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetLocationStatisticByIdHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetLocationStatisticByIdResponse> Handle(GetLocationStatisticById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var locationStatistic = await _statisticDbContext.FindLocationStatisticByIdAsync(command.Id);
        Guard.Against.NotFound(locationStatistic, new LocationStatisticNotFoundException(command.Id));

        var locationStatisticDto = _mapper.Map<LocationStatisticDto>(locationStatistic);

        return new GetLocationStatisticByIdResponse(locationStatisticDto);
    }
}
