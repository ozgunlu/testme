using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Domain;

public record ColorCreated(Color Color) : DomainEvent;

internal class ColorCreatedHandler : IDomainEventHandler<ColorCreated>
{
    private readonly VehicleDbContext _dbContext;

    public ColorCreatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(ColorCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class ColorCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<ColorCreated>
{
    public ColorCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(ColorCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
