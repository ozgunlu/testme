using DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record.Dtos;

namespace DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record;

public interface IRecordApiClient
{
    Task<GetRecordsResponse?> GetRecordsAsync(CancellationToken cancellationToken = default);
}
