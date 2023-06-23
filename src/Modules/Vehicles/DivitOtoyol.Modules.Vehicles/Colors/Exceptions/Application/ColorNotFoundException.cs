using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;

public class ColorNotFoundException : NotFoundException
{
    public ColorNotFoundException(long id) : base($"Araç Markası bulunamadı: '{id}'")
    {
    }

    public ColorNotFoundException(string message) : base(message)
    {
    }
}
