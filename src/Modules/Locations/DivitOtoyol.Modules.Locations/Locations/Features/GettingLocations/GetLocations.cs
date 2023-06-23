using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Locations.Locations.Dtos;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Locations.Features.GettingLocations;

public record GetLocations : ListQuery<GetLocationsResponse>
{
}

internal class GetLocationsValidator : AbstractValidator<GetLocations>
{
    public GetLocationsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetLocationsHandler : IQueryHandler<GetLocations, GetLocationsResponse>
{
    private readonly ILocationDbContext _locationDbContext;
    private readonly IMapper _mapper;

    public GetLocationsHandler(ILocationDbContext locationDbContext, IMapper mapper)
    {
        _locationDbContext = locationDbContext;
        _mapper = mapper;
    }

    public async Task<GetLocationsResponse> Handle(
        GetLocations request,
        CancellationToken cancellationToken)
    {
        var locations = await _locationDbContext.Locations
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Location, LocationDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetLocationsResponse(locations);
    }
}
