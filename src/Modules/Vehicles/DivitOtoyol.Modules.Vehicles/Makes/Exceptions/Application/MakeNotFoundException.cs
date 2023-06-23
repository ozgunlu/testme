using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;

public class MakeNotFoundException : NotFoundException
{
    public MakeNotFoundException(long id) : base($"Araç Markası bulunamadı: '{id}'")
    {
    }

    public MakeNotFoundException(string message) : base(message)
    {
    }
}
