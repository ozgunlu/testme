using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.DeletingMake;

public record DeleteMake(long Id) : ITxCommand;

internal class DeleteMakeValidator : AbstractValidator<DeleteMake>
{
    public DeleteMakeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteMakeHandler : ICommandHandler<DeleteMake>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly ILogger<DeleteMakeHandler> _logger;

    public DeleteMakeHandler(
        VehicleDbContext vehicleDbContext,
        ILogger<DeleteMakeHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteMake command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var make = await _vehicleDbContext.FindMakeAsync(command.Id);

        Guard.Against.NotFound(make, new MakeNotFoundException(command.Id));

        _vehicleDbContext.Makes.Remove(make!);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        make!.Delete();

        _logger.LogInformation("Make with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
