using DivitOtoyol.Modules.Locations.Locations.Protos;

namespace DivitOtoyol.Modules.Cameras.Shared.Location;

public interface ILocationGrpcClient
{
    Task<LocationResponse> GetLocationByIdAsync(long id, CancellationToken cancellationToken = default);
}
