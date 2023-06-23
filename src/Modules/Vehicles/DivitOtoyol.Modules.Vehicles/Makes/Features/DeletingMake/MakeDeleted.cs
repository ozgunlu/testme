using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.DeletingMake;

public record MakeDeleted(Make Make) : DomainEvent;

internal class MakeDeletedHandler : IDomainEventHandler<MakeDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly VehicleDbContext _vehicleDbContext;

    public MakeDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        VehicleDbContext vehicleDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _vehicleDbContext = vehicleDbContext;
    }

    public Task Handle(MakeDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
