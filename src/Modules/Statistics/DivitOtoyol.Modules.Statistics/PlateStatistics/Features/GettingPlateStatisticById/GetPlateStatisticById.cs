using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Application;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using DivitOtoyol.Modules.Statistics.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.GettingPlateStatisticById;

public record GetPlateStatisticById(long Id) : IQuery<GetPlateStatisticByIdResponse>;

internal class GetPlateStatisticByIdValidator : AbstractValidator<GetPlateStatisticById>
{
    public GetPlateStatisticByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetPlateStatisticByIdHandler : IQueryHandler<GetPlateStatisticById, GetPlateStatisticByIdResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetPlateStatisticByIdHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetPlateStatisticByIdResponse> Handle(GetPlateStatisticById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var plateStatistic = await _statisticDbContext.FindPlateStatisticByIdAsync(command.Id);
        Guard.Against.NotFound(plateStatistic, new PlateStatisticNotFoundException(command.Id));

        var plateStatisticDto = _mapper.Map<PlateStatisticDto>(plateStatistic);

        return new GetPlateStatisticByIdResponse(plateStatisticDto);
    }
}
