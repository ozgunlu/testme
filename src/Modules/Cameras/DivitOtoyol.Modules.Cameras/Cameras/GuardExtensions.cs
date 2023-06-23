using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Application;
namespace DivitOtoyol.Modules.Cameras.Cameras;

public static class GuardExtensions
{
    public static void ExistsCamera(this IGuardClause guardClause, bool exists, long cameraId)
    {
        if (exists == false)
            throw new CameraNotFoundException(cameraId);
    }
}
