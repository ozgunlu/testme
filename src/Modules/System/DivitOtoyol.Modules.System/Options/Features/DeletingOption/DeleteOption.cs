using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Shared.Data;
using DivitOtoyol.Modules.Systems.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Systems.Options.Features.DeletingOption;

public record DeleteOption(long Id) : ITxCommand;

internal class DeleteOptionValidator : AbstractValidator<DeleteOption>
{
    public DeleteOptionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteOptionHandler : ICommandHandler<DeleteOption>
{
    private readonly SystemDbContext _systemDbContext;
    private readonly ILogger<DeleteOptionHandler> _logger;

    public DeleteOptionHandler(
        SystemDbContext systemDbContext,
        ILogger<DeleteOptionHandler> logger)
    {
        _systemDbContext = systemDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOption command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));
        var option = await _systemDbContext.FindOptionAsync(command.Id);

        Guard.Against.NotFound(option, new OptionNotFoundException(command.Id));

        _systemDbContext.Options.Remove(option!);

        await _systemDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        option!.Delete();

        _logger.LogInformation("Option with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
