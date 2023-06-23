using DivitOtoyol.Modules.PlateRecognitions.Shared.Camera.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Camera;

// Ref: http://www.kamilgrzybek.com/design/modular-monolith-integration-styles/
// https://docs.microsoft.com/en-us/azure/architecture/patterns/anti-corruption-layer
public interface ICameraApiClient
{
    Task<GetCameraByIdResponse?> GetCameraByIdAsync(long id, CancellationToken cancellationToken = default);
}
