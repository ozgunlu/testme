using System.Net;
using System.Net.Http.Json;
using Ardalis.GuardClauses;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model.Dtos;
using Microsoft.Extensions.Options;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model;

public class ModelApiClient : IModelApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ModelsApiClientOptions _options;

    public ModelApiClient(HttpClient httpClient, IOptions<ModelsApiClientOptions> options)
    {
        _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
        _options = Guard.Against.Null(options.Value, nameof(options));

        if (string.IsNullOrEmpty(_options.BaseApiAddress) == false)
            _httpClient.BaseAddress = new Uri(_options.BaseApiAddress);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<CreateModelResponse> CreateModelAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));

        var httpContent = JsonContent.Create(new { Name = name });

        var response = await _httpClient.PostAsync(
            $"{_options.ModelsEndpoint}",
            httpContent,
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CreateModelResponse>();
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
