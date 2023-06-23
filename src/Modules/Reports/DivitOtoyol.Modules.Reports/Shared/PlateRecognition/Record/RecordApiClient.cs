using System.Net.Http.Json;
using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record.Dtos;
using Microsoft.Extensions.Options;

namespace DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record;
public class RecordApiClient : IRecordApiClient
{
    private readonly HttpClient _httpClient;
    private readonly RecordsApiClientOptions _options;

    public RecordApiClient(HttpClient httpClient, IOptions<RecordsApiClientOptions> options)
    {
        _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
        _options = Guard.Against.Null(options.Value, nameof(options));

        if (string.IsNullOrEmpty(_options.BaseApiAddress) == false)
            _httpClient.BaseAddress = new Uri(_options.BaseApiAddress);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public async Task<GetRecordsResponse?> GetRecordsAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<GetRecordsResponse>(
            $"{_options.RecordsEndpoint}",
            cancellationToken);

        return response;
    }
}
