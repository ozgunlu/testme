using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class CameraDbContextExtensions
{
    public static Task<bool> CameraExistsAsync(
        this ICameraDbContext context,
        CameraId id,
        CancellationToken cancellationToken = default)
    {
        return context.Cameras.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Camera?> FindCameraAsync(
        this ICameraDbContext context,
        CameraId id)
    {
        return context.Cameras.FindAsync(id);
    }
}
