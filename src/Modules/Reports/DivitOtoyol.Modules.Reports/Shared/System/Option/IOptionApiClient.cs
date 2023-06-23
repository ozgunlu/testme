using DivitOtoyol.Modules.Reports.Shared.System.Option.Dtos;

namespace DivitOtoyol.Modules.Reports.Shared.System.Option;

public interface IOptionApiClient
{
    Task<GetOptionByKeyResponse?> GetOptionByKeyAsync(string key, CancellationToken cancellationToken = default);
}
