using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.DeletingRecord;

public record RecordDeleted(Record Record) : DomainEvent;

internal class RecordDeletedHandler : IDomainEventHandler<RecordDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly PlateRecognitionDbContext _plateRecognitionDbContext;

    public RecordDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        PlateRecognitionDbContext plateRecognitionDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _plateRecognitionDbContext = plateRecognitionDbContext;
    }

    public Task Handle(RecordDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
