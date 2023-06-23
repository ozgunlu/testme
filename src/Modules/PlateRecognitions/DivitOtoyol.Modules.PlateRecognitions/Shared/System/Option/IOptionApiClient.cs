using DivitOtoyol.Modules.PlateRecognitions.Shared.System.Option.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.System.Option;

public interface IOptionApiClient
{
    Task<GetOptionByKeyResponse> GetOptionByKeyAsync(string key, CancellationToken cancellationToken = default);

    Task<GetActiveBaseFolderResponse> GetActiveBaseFolderAsync(CancellationToken cancellationToken = default);
}
