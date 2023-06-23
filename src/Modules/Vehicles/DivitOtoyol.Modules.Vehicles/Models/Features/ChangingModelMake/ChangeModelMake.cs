using BuildingBlocks.Abstractions.CQRS.Command;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelMake;

internal record ChangeModelMake : ITxCommand<ChangeModelMakeResponse>;

internal class ChangeModelMakeHandler :
    ICommandHandler<ChangeModelMake, ChangeModelMakeResponse>
{
    public Task<ChangeModelMakeResponse> Handle(
        ChangeModelMake command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<ChangeModelMakeResponse>(null!);
    }
}
