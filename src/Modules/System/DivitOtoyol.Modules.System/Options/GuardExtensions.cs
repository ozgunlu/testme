using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;

namespace DivitOtoyol.Modules.Systems.Options;

public static class GuardExtensions
{
    public static void ExistsOption(this IGuardClause guardClause, bool exists, long optionId)
    {
        if (exists == false)
            throw new OptionNotFoundException(optionId);
    }

    public static void ExistsOptionByKey(this IGuardClause guardClause, bool exists, string key)
    {
        if (exists == false)
            throw new OptionNotFoundException(key);
    }
}
