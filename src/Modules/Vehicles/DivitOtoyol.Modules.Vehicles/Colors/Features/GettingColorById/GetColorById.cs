using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Colors.Dtos;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.GettingColorById;

public record GetColorById(long Id) : IQuery<GetColorByIdResponse>;

internal class GetColorByIdValidator : AbstractValidator<GetColorById>
{
    public GetColorByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetColorByIdHandler : IQueryHandler<GetColorById, GetColorByIdResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetColorByIdHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetColorByIdResponse> Handle(GetColorById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var color = await _vehicleDbContext.FindColorAsync(command.Id);
        Guard.Against.NotFound(color, new ColorNotFoundException(command.Id));

        var colorDto = _mapper.Map<ColorDto>(color);

        return new GetColorByIdResponse(colorDto);
    }
}
