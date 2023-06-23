using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;

namespace DivitOtoyol.Modules.Locations.Locations;

public static class GuardExtensions
{
    public static void ExistsLocation(this IGuardClause guardClause, bool exists, long locationId)
    {
        if (exists == false)
            throw new LocationNotFoundException(locationId);
    }
}
