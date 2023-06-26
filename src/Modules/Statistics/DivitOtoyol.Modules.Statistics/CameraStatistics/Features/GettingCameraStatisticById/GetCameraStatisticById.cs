using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Application;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using DivitOtoyol.Modules.Statistics.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.GettingCameraStatisticById;

public record GetCameraStatisticById(long Id) : IQuery<GetCameraStatisticByIdResponse>;

internal class GetCameraStatisticByIdValidator : AbstractValidator<GetCameraStatisticById>
{
    public GetCameraStatisticByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetCameraStatisticByIdHandler : IQueryHandler<GetCameraStatisticById, GetCameraStatisticByIdResponse>
{
    private readonly IStatisticDbContext _statisticDbContext;
    private readonly IMapper _mapper;

    public GetCameraStatisticByIdHandler(IStatisticDbContext statisticDbContext, IMapper mapper)
    {
        _statisticDbContext = statisticDbContext;
        _mapper = mapper;
    }

    public async Task<GetCameraStatisticByIdResponse> Handle(GetCameraStatisticById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var cameraStatistic = await _statisticDbContext.FindCameraStatisticByIdAsync(command.Id);
        Guard.Against.NotFound(cameraStatistic, new CameraStatisticNotFoundException(command.Id));

        var cameraStatisticDto = _mapper.Map<CameraStatisticDto>(cameraStatistic);

        return new GetCameraStatisticByIdResponse(cameraStatisticDto);
    }
}
