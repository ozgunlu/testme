using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Systems.Options.Exceptions.Application;

public class OptionNotFoundException : NotFoundException
{
    public OptionNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public OptionNotFoundException(string message) : base(message)
    {
    }
}
