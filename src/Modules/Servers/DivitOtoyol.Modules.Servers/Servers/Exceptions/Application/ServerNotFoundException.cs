using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Servers.Servers.Exceptions.Application;

public class ServerNotFoundException : NotFoundException
{
    public ServerNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public ServerNotFoundException(string message) : base(message)
    {
    }
}
