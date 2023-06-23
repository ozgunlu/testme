using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Data;

namespace DivitOtoyol.Modules.Locations.Locations.Features.DeletingLocation;

public record LocationDeleted(Location Location) : DomainEvent;

internal class LocationDeletedHandler : IDomainEventHandler<LocationDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly LocationDbContext _locationDbContext;

    public LocationDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        LocationDbContext locationDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _locationDbContext = locationDbContext;
    }

    public Task Handle(LocationDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
