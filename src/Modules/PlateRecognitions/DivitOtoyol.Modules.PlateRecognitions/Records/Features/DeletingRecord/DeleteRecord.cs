using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.DeletingRecord;

public record DeleteRecord(long Id) : ITxCommand;

internal class DeleteRecordValidator : AbstractValidator<DeleteRecord>
{
    public DeleteRecordValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteRecordHandler : ICommandHandler<DeleteRecord>
{
    private readonly PlateRecognitionDbContext _plateRecognitionDbContext;
    private readonly ILogger<DeleteRecordHandler> _logger;

    public DeleteRecordHandler(
        PlateRecognitionDbContext plateRecognitionDbContext,
        ILogger<DeleteRecordHandler> logger)
    {
        _plateRecognitionDbContext = plateRecognitionDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteRecord command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var record = await _plateRecognitionDbContext.FindRecordAsync(command.Id);

        Guard.Against.NotFound(record, new RecordNotFoundException(command.Id));

        _plateRecognitionDbContext.Records.Remove(record!);

        await _plateRecognitionDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        record!.Delete();

        _logger.LogInformation("Record with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
