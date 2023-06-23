using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.UpdatingRecord;

public record UpdateRecord(
    long Id,
    string Plate) : ITxUpdateCommand;

internal class UpdateRecordValidator : AbstractValidator<UpdateRecord>
{
    public UpdateRecordValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Plate).NotEmpty();
    }
}

internal class UpdateRecordCommandHandler : ICommandHandler<UpdateRecord>
{
    private readonly IPlateRecognitionDbContext _plateRecognitionDbContext;

    public UpdateRecordCommandHandler(IPlateRecognitionDbContext plateRecognitionDbContext)
    {
        _plateRecognitionDbContext = plateRecognitionDbContext;
    }

    public async Task<Unit> Handle(UpdateRecord command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var record = await _plateRecognitionDbContext.FindRecordAsync(command.Id);
        Guard.Against.NotFound(record, new RecordNotFoundException(command.Id));

        record!.ChangePlate(command.Plate);

        await _plateRecognitionDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
