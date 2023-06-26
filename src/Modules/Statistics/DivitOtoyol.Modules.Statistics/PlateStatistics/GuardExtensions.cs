using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Application;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics;

public static class GuardExtensions
{
    public static void ExistsPlateStatistic(this IGuardClause guardClause, bool exists, long plateStatisticId)
    {
        if (exists == false)
            throw new PlateStatisticNotFoundException(plateStatisticId);
    }
}
