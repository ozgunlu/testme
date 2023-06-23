using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Application;
namespace DivitOtoyol.Modules.Servers.Servers;

public static class GuardExtensions
{
    public static void ExistsServer(this IGuardClause guardClause, bool exists, long serverId)
    {
        if (exists == false)
            throw new ServerNotFoundException(serverId);
    }
}
