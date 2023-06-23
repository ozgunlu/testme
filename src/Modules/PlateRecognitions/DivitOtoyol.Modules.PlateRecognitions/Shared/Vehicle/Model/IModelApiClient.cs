using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model;

public interface IModelApiClient
{
    Task<CreateModelResponse> CreateModelAsync(string name, CancellationToken cancellationToken = default);
}
