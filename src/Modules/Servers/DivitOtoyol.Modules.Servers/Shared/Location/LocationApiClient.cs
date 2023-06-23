using System.Net.Http.Json;
using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Servers.Shared.Location.Dtos;
using Microsoft.Extensions.Options;

namespace DivitOtoyol.Modules.Servers.Shared.Location;

// Ref: http://www.kamilgrzybek.com/design/modular-monolith-integration-styles/
// https://docs.microsoft.com/en-us/azure/architecture/patterns/anti-corruption-layer
public class LocationApiClient : ILocationApiClient
{
    private readonly HttpClient _httpClient;
    private readonly LocationsApiClientOptions _options;

    public LocationApiClient(HttpClient httpClient, IOptions<LocationsApiClientOptions> options)
    {
        _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
        _options = Guard.Against.Null(options.Value, nameof(options));

        if (string.IsNullOrEmpty(_options.BaseApiAddress) == false)
            _httpClient.BaseAddress = new Uri(_options.BaseApiAddress);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<GetLocationByIdResponse?> GetLocationByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.NegativeOrZero(id, nameof(id));

        var response = await _httpClient.GetFromJsonAsync<GetLocationByIdResponse>(
            $"{_options.LocationsEndpoint}/{id}",
            cancellationToken);

        return response;
    }
}
