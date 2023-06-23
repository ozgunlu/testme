using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.DeletingType;

public record DeleteType(long Id) : ITxCommand;

internal class DeleteTypeValidator : AbstractValidator<DeleteType>
{
    public DeleteTypeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteTypeHandler : ICommandHandler<DeleteType>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly ILogger<DeleteTypeHandler> _logger;

    public DeleteTypeHandler(
        VehicleDbContext vehicleDbContext,
        ILogger<DeleteTypeHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteType command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var type = await _vehicleDbContext.FindTypeAsync(command.Id);

        Guard.Against.NotFound(type, new TypeNotFoundException(command.Id));

        _vehicleDbContext.VehicleTypes.Remove(type!);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        type!.Delete();

        _logger.LogInformation("Type with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
