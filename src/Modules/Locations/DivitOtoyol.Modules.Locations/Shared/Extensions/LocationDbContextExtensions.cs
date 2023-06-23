using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Locations.ValueObjects;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class LocationDbContextExtensions
{
    public static Task<bool> LocationExistsAsync(
        this ILocationDbContext context,
        LocationId id,
        CancellationToken cancellationToken = default)
    {
        return context.Locations.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Location?> FindLocationAsync(
        this ILocationDbContext context,
        LocationId id)
    {
        return context.Locations.FindAsync(id);
    }
}
