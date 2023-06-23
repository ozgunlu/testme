using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Color.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Color;

public interface IColorApiClient
{
    Task<CreateColorResponse> CreateColorAsync(string name, CancellationToken cancellationToken = default);
}
