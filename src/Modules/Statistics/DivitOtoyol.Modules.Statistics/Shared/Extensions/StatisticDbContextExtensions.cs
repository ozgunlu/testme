using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;
using DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.Shared.Extensions;

public static class StatisticDbContextExtensions
{
    public static Task<bool> CameraStatisticExistsAsync(
        this IStatisticDbContext context,
        CameraStatisticId id,
        CancellationToken cancellationToken = default)
    {
        return context.CameraStatistics.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<CameraStatistic?> FindCameraStatisticAsync(
        this IStatisticDbContext context,
        CameraStatisticId id)
    {
        return context.CameraStatistics.FindAsync(id);
    }

    public static ValueTask<CameraStatistic?> FindCameraStatisticByIdAsync(
        this IStatisticDbContext context,
        CameraStatisticId id)
    {
        return context.CameraStatistics.FindAsync(id);
    }

    public static Task<bool> LocationStatisticExistsAsync(
        this IStatisticDbContext context,
        LocationStatisticId id,
        CancellationToken cancellationToken = default)
    {
        return context.LocationStatistics.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<LocationStatistic?> FindLocationStatisticAsync(
        this IStatisticDbContext context,
        LocationStatisticId id)
    {
        return context.LocationStatistics.FindAsync(id);
    }

    public static ValueTask<LocationStatistic?> FindLocationStatisticByIdAsync(
        this IStatisticDbContext context,
        LocationStatisticId id)
    {
        return context.LocationStatistics.FindAsync(id);
    }

    public static Task<bool> PlateStatisticExistsAsync(
        this IStatisticDbContext context,
        PlateStatisticId id,
        CancellationToken cancellationToken = default)
    {
        return context.PlateStatistics.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public static ValueTask<PlateStatistic?> FindPlateStatisticAsync(
        this IStatisticDbContext context,
        PlateStatisticId id)
    {
        return context.PlateStatistics.FindAsync(id);
    }

    public static ValueTask<PlateStatistic?> FindPlateStatisticByIdAsync(
        this IStatisticDbContext context,
        PlateStatisticId id)
    {
        return context.PlateStatistics.FindAsync(id);
    }
}
