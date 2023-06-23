using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class ServerDbContextExtensions
{
    public static Task<bool> ServerExistsAsync(
        this IServerDbContext context,
        ServerId id,
        CancellationToken cancellationToken = default)
    {
        return context.Servers.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Server?> FindServerAsync(
        this IServerDbContext context,
        ServerId id)
    {
        return context.Servers.FindAsync(id);
    }

    public static ValueTask<Server?> FindServerByIdAsync(
        this IServerDbContext context,
        ServerId id)
    {
        return context.Servers.FindAsync(id);
    }
}
