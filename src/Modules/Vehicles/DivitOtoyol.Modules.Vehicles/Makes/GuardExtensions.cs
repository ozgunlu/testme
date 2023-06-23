using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;

namespace DivitOtoyol.Modules.Vehicles.Makes;

public static class GuardExtensions
{
    public static void ExistsMake(this IGuardClause guardClause, bool exists, long makeId)
    {
        if (exists == false)
            throw new MakeNotFoundException(makeId);
    }
}
