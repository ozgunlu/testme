using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Dtos;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.GettingMakeById;

public record GetMakeById(long Id) : IQuery<GetMakeByIdResponse>;

internal class GetMakeByIdValidator : AbstractValidator<GetMakeById>
{
    public GetMakeByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

internal class GetMakeByIdHandler : IQueryHandler<GetMakeById, GetMakeByIdResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetMakeByIdHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetMakeByIdResponse> Handle(GetMakeById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var make = await _vehicleDbContext.FindMakeAsync(command.Id);
        Guard.Against.NotFound(make, new MakeNotFoundException(command.Id));

        var makeDto = _mapper.Map<MakeDto>(make);

        return new GetMakeByIdResponse(makeDto);
    }
}
