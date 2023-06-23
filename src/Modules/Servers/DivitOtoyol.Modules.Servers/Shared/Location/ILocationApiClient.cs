using DivitOtoyol.Modules.Servers.Shared.Location.Dtos;

namespace DivitOtoyol.Modules.Servers.Shared.Location;

// Ref: http://www.kamilgrzybek.com/design/modular-monolith-integration-styles/
// https://docs.microsoft.com/en-us/azure/architecture/patterns/anti-corruption-layer
public interface ILocationApiClient
{
    Task<GetLocationByIdResponse?> GetLocationByIdAsync(long id, CancellationToken cancellationToken = default);
}
