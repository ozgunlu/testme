using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Type = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.DeletingType;

public record TypeDeleted(Type Type) : DomainEvent;

internal class TypeDeletedHandler : IDomainEventHandler<TypeDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly VehicleDbContext _vehicleDbContext;

    public TypeDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        VehicleDbContext vehicleDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _vehicleDbContext = vehicleDbContext;
    }

    public Task Handle(TypeDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
