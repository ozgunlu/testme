using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using BuildingBlocks.Persistence.Mongo;
using DivitOtoyol.Modules.Vehicles.Colors.Dtos;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.GettingColors;

public record GetColors : ListQuery<GetColorsResponse>
{
}

internal class GetColorsValidator : AbstractValidator<GetColors>
{
    public GetColorsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetColorsHandler : IQueryHandler<GetColors, GetColorsResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetColorsHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetColorsResponse> Handle(
        GetColors request,
        CancellationToken cancellationToken)
    {
        var colors = await _vehicleDbContext.Colors
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Color, ColorDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetColorsResponse(colors);
    }
}
