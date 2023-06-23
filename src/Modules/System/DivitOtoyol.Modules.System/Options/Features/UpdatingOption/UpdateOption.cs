using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using DivitOtoyol.Modules.Systems.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Systems.Options.Features.UpdatingOption;

public record UpdateOption(
    long Id,
    string Key,
    string Value,
    string Modules,
    bool CanUpdate,
    bool CanDelete) : ITxUpdateCommand;

internal class UpdateOptionValidator : AbstractValidator<UpdateOption>
{
    public UpdateOptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Key).NotEmpty();

        RuleFor(x => x.Value).NotEmpty();

        RuleFor(x => x.Modules).NotEmpty();
    }
}

internal class UpdateOptionCommandHandler : ICommandHandler<UpdateOption>
{
    private readonly ISystemDbContext _systemDbContext;

    public UpdateOptionCommandHandler(ISystemDbContext systemDbContext)
    {
        _systemDbContext = systemDbContext;
    }

    public async Task<Unit> Handle(UpdateOption command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var option = await _systemDbContext.FindOptionAsync(command.Id);
        Guard.Against.NotFound(option, new OptionNotFoundException(command.Id));

        option!.ChangeKey(command.Key);
        option!.ChangeValue(command.Value);
        option!.ChangeModules(command.Modules);
        option!.ChangeCanUpdate(command.CanUpdate);
        option!.ChangeCanDelete(command.CanDelete);

        await _systemDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
