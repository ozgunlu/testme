using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Colors.Exceptions;

public class ColorNotFoundException : NotFoundException
{
    public ColorNotFoundException(long id) : base($"Color with id {id} not found")
    {
    }
}
