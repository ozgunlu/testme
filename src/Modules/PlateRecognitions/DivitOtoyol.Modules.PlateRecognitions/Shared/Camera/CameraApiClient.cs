using System.Net;
using System.Net.Http.Json;
using Ardalis.GuardClauses;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Camera.Dtos;
using Microsoft.Extensions.Options;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Camera;

public class CameraApiClient : ICameraApiClient
{
    private readonly HttpClient _httpClient;
    private readonly CamerasApiClientOptions _options;

    public CameraApiClient(HttpClient httpClient, IOptions<CamerasApiClientOptions> options)
    {
        _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
        _options = Guard.Against.Null(options.Value, nameof(options));

        if (string.IsNullOrEmpty(_options.BaseApiAddress) == false)
            _httpClient.BaseAddress = new Uri(_options.BaseApiAddress);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<GetCameraByIdResponse?> GetCameraByIdAsync(
    long id,
    CancellationToken cancellationToken = default)
    {
        Guard.Against.NegativeOrZero(id, nameof(id));

        var response = await _httpClient.GetAsync(
            $"{_options.CamerasEndpoint}/{id}",
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GetCameraByIdResponse>();
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        else
        {
            throw new Exception($"Unexpected status code: {response.StatusCode}");
        }
    }

}
