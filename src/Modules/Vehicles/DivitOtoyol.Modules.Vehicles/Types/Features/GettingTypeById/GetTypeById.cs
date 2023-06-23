using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using DivitOtoyol.Modules.Vehicles.Types.Dtos;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.GettingTypeById;

public record GetTypeById(long Id) : IQuery<GetTypeByIdResponse>;

internal class GetTypeByIdValidator : AbstractValidator<GetTypeById>
{
    public GetTypeByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetTypeByIdHandler : IQueryHandler<GetTypeById, GetTypeByIdResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetTypeByIdHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetTypeByIdResponse> Handle(GetTypeById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var type = await _vehicleDbContext.FindTypeAsync(command.Id);
        Guard.Against.NotFound(type, new TypeNotFoundException(command.Id));

        var typeDto = _mapper.Map<TypeDto>(type);

        return new GetTypeByIdResponse(typeDto);
    }
}
