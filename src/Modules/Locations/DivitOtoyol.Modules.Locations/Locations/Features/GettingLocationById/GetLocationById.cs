using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Locations.Locations.Dtos;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using DivitOtoyol.Modules.Locations.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Locations.Locations.Features.GettingLocationById;

public record GetLocationById(long Id) : IQuery<GetLocationByIdResponse>;

internal class GetLocationByIdValidator : AbstractValidator<GetLocationById>
{
    public GetLocationByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetLocationByIdHandler : IQueryHandler<GetLocationById, GetLocationByIdResponse>
{
    private readonly ILocationDbContext _locationDbContext;
    private readonly IMapper _mapper;

    public GetLocationByIdHandler(ILocationDbContext locationDbContext, IMapper mapper)
    {
        _locationDbContext = locationDbContext;
        _mapper = mapper;
    }

    public async Task<GetLocationByIdResponse> Handle(GetLocationById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var location = await _locationDbContext.FindLocationAsync(command.Id);
        Guard.Against.NotFound(location, new LocationNotFoundException(command.Id));

        var locationDto = _mapper.Map<LocationDto>(location);

        return new GetLocationByIdResponse(locationDto);
    }
}
