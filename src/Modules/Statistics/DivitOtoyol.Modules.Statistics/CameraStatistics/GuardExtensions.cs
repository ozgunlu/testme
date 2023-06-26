using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Application;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics;

public static class GuardExtensions
{
    public static void ExistsCameraStatistic(this IGuardClause guardClause, bool exists, long cameraStatisticId)
    {
        if (exists == false)
            throw new CameraStatisticNotFoundException(cameraStatisticId);
    }
}
