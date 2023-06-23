using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using DivitOtoyol.Modules.Locations.Shared.Extensions;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Locations.Locations.Features.UpdatingLocation;

public record UpdateLocation(
    long Id,
    string Name,
    long ParentId) : ITxUpdateCommand;

internal class UpdateLocationValidator : AbstractValidator<UpdateLocation>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class UpdateLocationCommandHandler : ICommandHandler<UpdateLocation>
{
    private readonly ILocationDbContext _locationDbContext;

    public UpdateLocationCommandHandler(ILocationDbContext locationDbContext)
    {
        _locationDbContext = locationDbContext;
    }

    public async Task<Unit> Handle(UpdateLocation command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var location = await _locationDbContext.FindLocationAsync(command.Id);
        Guard.Against.NotFound(location, new LocationNotFoundException(command.Id));

        location!.ChangeName(command.Name);

        var parentLocation = await _locationDbContext.FindLocationAsync(command.ParentId);
        Guard.Against.NotFound(parentLocation, new LocationNotFoundException(command.ParentId));

        location.SetParent(command.ParentId);

        await _locationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
