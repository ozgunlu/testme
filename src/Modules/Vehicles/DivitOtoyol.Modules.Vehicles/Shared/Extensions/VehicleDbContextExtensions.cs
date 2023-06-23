using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Colors.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Models.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;
using Microsoft.EntityFrameworkCore;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Shared.Extensions;

/// <summary>
/// Put some shared code between multiple feature here, for preventing duplicate some codes
/// Ref: https://www.youtube.com/watch?v=01lygxvbao4.
/// </summary>
public static class VehicleDbContextExtensions
{
    public static Task<bool> TypeExistsAsync(
        this IVehicleDbContext context,
        TypeId id,
        CancellationToken cancellationToken = default)
    {
        return context.VehicleTypes.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<VehicleType?> FindTypeAsync(
        this IVehicleDbContext context,
        TypeId id)
    {
        return context.VehicleTypes.FindAsync(id);
    }

    public static Task<bool> MakeExistsAsync(
        this IVehicleDbContext context,
        MakeId id,
        CancellationToken cancellationToken = default)
    {
        return context.Makes.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Make?> FindMakeAsync(
        this IVehicleDbContext context,
        MakeId id)
    {
        return context.Makes.FindAsync(id);
    }

    public static Task<bool> ModelExistsAsync(
        this IVehicleDbContext context,
        ModelId id,
        CancellationToken cancellationToken = default)
    {
        return context.Models.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Model?> FindModelAsync(
        this IVehicleDbContext context,
        ModelId id)
    {
        return context.Models.FindAsync(id);
    }

    public static Task<bool> ColorExistsAsync(
        this IVehicleDbContext context,
        ColorId id,
        CancellationToken cancellationToken = default)
    {
        return context.Colors.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<Color?> FindColorAsync(
        this IVehicleDbContext context,
        ColorId id)
    {
        return context.Colors.FindAsync(id);
    }
}
