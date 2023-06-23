using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.UpdatingRecord;

public record RecordUpdated(Record Record) : DomainEvent;

public class RecordUpdatedHandler : IDomainEventHandler<RecordUpdated>
{
    private readonly PlateRecognitionDbContext _dbContext;

    public RecordUpdatedHandler(PlateRecognitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(RecordUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
