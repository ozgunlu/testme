using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Options.ValueObjects;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class SystemDbContextExtensions
{
    public static Task<bool> OptionExistsAsync(
        this ISystemDbContext context,
        OptionId id,
        CancellationToken cancellationToken = default)
    {
        return context.Options.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Option?> FindOptionAsync(
        this ISystemDbContext context,
        OptionId id)
    {
        return context.Options.FindAsync(id);
    }
}
