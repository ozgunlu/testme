using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Models.Dtos;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.GettingModelById;

public record GetModelById(long Id) : IQuery<GetModelByIdResponse>;

internal class GetModelByIdValidator : AbstractValidator<GetModelById>
{
    public GetModelByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

internal class GetModelByIdHandler : IQueryHandler<GetModelById, GetModelByIdResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetModelByIdHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetModelByIdResponse> Handle(GetModelById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var model = await _vehicleDbContext.FindModelAsync(command.Id);
        Guard.Against.NotFound(model, new ModelNotFoundException(command.Id));

        var modelDto = _mapper.Map<ModelDto>(model);

        return new GetModelByIdResponse(modelDto);
    }
}
