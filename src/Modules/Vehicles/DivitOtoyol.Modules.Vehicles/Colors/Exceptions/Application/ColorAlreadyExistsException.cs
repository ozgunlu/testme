using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;

internal class ColorAlreadyExistsException : AppException
{
    public ColorAlreadyExistsException(string message) : base(message)
    {
    }
}
