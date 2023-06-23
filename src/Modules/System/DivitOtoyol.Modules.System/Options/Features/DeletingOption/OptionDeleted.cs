using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Data;

namespace DivitOtoyol.Modules.Systems.Options.Features.DeletingOption;

public record OptionDeleted(Option Option) : DomainEvent;

internal class OptionDeletedHandler : IDomainEventHandler<OptionDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly SystemDbContext _systemDbContext;

    public OptionDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        SystemDbContext systemDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _systemDbContext = systemDbContext;
    }

    public Task Handle(OptionDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
