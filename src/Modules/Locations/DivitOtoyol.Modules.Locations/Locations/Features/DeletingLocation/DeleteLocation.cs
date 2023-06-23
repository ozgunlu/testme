using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Locations.Shared.Data;
using DivitOtoyol.Modules.Locations.Shared.Extensions;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Locations.Locations.Features.DeletingLocation;

public record DeleteLocation(long Id) : ITxCommand;

internal class DeleteLocationValidator : AbstractValidator<DeleteLocation>
{
    public DeleteLocationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteLocationHandler : ICommandHandler<DeleteLocation>
{
    private readonly LocationDbContext _locationDbContext;
    private readonly ILogger<DeleteLocationHandler> _logger;

    public DeleteLocationHandler(
        LocationDbContext locationDbContext,
        ILogger<DeleteLocationHandler> logger)
    {
        _locationDbContext = locationDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLocation command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));
        var location = await _locationDbContext.FindLocationAsync(command.Id);

        Guard.Against.NotFound(location, new LocationNotFoundException(command.Id));

        _locationDbContext.Locations.Remove(location!);

        await _locationDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        location!.Delete();

        _logger.LogInformation("Location with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
