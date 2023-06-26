using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.Communications.Colors.Exceptions;

public class ColorNotFoundException : NotFoundException
{
    public ColorNotFoundException(long id) : base($"Color with id {id} not found")
    {
    }
}
