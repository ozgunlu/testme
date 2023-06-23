using Ardalis.GuardClauses;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;
namespace DivitOtoyol.Modules.PlateRecognitions.Records;

public static class GuardExtensions
{
    public static void ExistsRecord(this IGuardClause guardClause, bool exists, long recordId)
    {
        if (exists == false)
            throw new RecordNotFoundException(recordId);
    }
}
