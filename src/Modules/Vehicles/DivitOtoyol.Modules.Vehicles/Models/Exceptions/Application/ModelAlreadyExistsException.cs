using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;

internal class ModelAlreadyExistsException : AppException
{
    public ModelAlreadyExistsException(string message) : base(message)
    {
    }
}
