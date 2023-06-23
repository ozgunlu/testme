using BuildingBlocks.Abstractions.CQRS.Command;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelType;

internal record ChangeModelType : ITxCommand<ChangeModelTypeResponse>;

internal class ChangeModelTypeHandler :
    ICommandHandler<ChangeModelType, ChangeModelTypeResponse>
{
    public Task<ChangeModelTypeResponse> Handle(
        ChangeModelType command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<ChangeModelTypeResponse>(null!);
    }
}
