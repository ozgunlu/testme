using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.DeletingColor;

public record ColorDeleted(Color Color) : DomainEvent;

internal class ColorDeletedHandler : IDomainEventHandler<ColorDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly VehicleDbContext _vehicleDbContext;

    public ColorDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        VehicleDbContext vehicleDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _vehicleDbContext = vehicleDbContext;
    }

    public Task Handle(ColorDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
