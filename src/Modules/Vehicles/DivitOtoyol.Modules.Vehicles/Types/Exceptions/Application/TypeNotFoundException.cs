using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;

public class TypeNotFoundException : NotFoundException
{
    public TypeNotFoundException(long id) : base($"Araç Tipi bulunamadı: '{id}'")
    {
    }

    public TypeNotFoundException(string message) : base(message)
    {
    }
}
