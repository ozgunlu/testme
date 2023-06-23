using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.DeletingModel;

public record ModelDeleted(Model Model) : DomainEvent;

internal class ModelDeletedHandler : IDomainEventHandler<ModelDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly VehicleDbContext _vehicleDbContext;

    public ModelDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        VehicleDbContext vehicleDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _vehicleDbContext = vehicleDbContext;
    }

    public Task Handle(ModelDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
