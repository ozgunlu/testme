using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.DeletingColor;

public record DeleteColor(long Id) : ITxCommand;

internal class DeleteColorValidator : AbstractValidator<DeleteColor>
{
    public DeleteColorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteColorHandler : ICommandHandler<DeleteColor>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly ILogger<DeleteColorHandler> _logger;

    public DeleteColorHandler(
        VehicleDbContext vehicleDbContext,
        ILogger<DeleteColorHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteColor command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var color = await _vehicleDbContext.FindColorAsync(command.Id);

        Guard.Against.NotFound(color, new ColorNotFoundException(command.Id));

        _vehicleDbContext.Colors.Remove(color!);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        color!.Delete();

        _logger.LogInformation("Color with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
