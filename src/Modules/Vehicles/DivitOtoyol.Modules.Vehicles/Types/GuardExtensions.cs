using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;

namespace DivitOtoyol.Modules.Vehicles.Types;

public static class GuardExtensions
{
    public static void ExistsType(this IGuardClause guardClause, bool exists, long typeId)
    {
        if (exists == false)
            throw new TypeNotFoundException(typeId);
    }
}
