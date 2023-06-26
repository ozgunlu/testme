using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Application;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics;

public static class GuardExtensions
{
    public static void ExistsLocationStatistic(this IGuardClause guardClause, bool exists, long locationStatisticId)
    {
        if (exists == false)
            throw new LocationStatisticNotFoundException(locationStatisticId);
    }
}
