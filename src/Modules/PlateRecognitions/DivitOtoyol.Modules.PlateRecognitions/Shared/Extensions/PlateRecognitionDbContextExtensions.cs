using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class PlateRecognitionDbContextExtensions
{
    public static Task<bool> RecordExistsAsync(
        this IPlateRecognitionDbContext context,
        RecordId id,
        CancellationToken cancellationToken = default)
    {
        return context.Records.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Record?> FindRecordAsync(
        this IPlateRecognitionDbContext context,
        RecordId id)
    {
        return context.Records.FindAsync(id);
    }
}
