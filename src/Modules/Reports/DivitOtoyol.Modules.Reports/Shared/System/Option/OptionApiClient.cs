using System.Net;
using System.Net.Http.Json;
using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Reports.Shared.System.Option.Dtos;
using Microsoft.Extensions.Options;

namespace DivitOtoyol.Modules.Reports.Shared.System.Option;
public class OptionApiClient : IOptionApiClient
{
    private readonly HttpClient _httpClient;
    private readonly OptionsApiClientOptions _options;

    public OptionApiClient(HttpClient httpClient, IOptions<OptionsApiClientOptions> options)
    {
        _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
        _options = Guard.Against.Null(options.Value, nameof(options));

        if (string.IsNullOrEmpty(_options.BaseApiAddress) == false)
            _httpClient.BaseAddress = new Uri(_options.BaseApiAddress);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<GetOptionByKeyResponse?> GetOptionByKeyAsync(
        string key,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"{_options.OptionsEndpoint}/key/{key}",
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GetOptionByKeyResponse>();
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
