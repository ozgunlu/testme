using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;

internal class TypeAlreadyExistsException : AppException
{
    public TypeAlreadyExistsException(string message) : base(message)
    {
    }
}
