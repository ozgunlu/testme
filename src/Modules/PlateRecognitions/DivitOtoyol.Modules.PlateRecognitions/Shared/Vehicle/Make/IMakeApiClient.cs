using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Make.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Make;

public interface IMakeApiClient
{
    Task<CreateMakeResponse> CreateMakeAsync(string name, CancellationToken cancellationToken = default);
}
