using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;

namespace DivitOtoyol.Modules.Vehicles.Colors;

public static class GuardExtensions
{
    public static void ExistsColor(this IGuardClause guardClause, bool exists, long colorId)
    {
        if (exists == false)
            throw new ColorNotFoundException(colorId);
    }
}
