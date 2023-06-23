using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;

namespace DivitOtoyol.Modules.Vehicles.Models;

public static class GuardExtensions
{
    public static void ExistsModel(this IGuardClause guardClause, bool exists, long modelId)
    {
        if (exists == false)
            throw new ModelNotFoundException(modelId);
    }
}
