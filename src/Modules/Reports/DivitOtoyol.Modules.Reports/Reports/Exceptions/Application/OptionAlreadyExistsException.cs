using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Systems.Options.Exceptions.Application;

internal class OptionAlreadyExistsException : AppException
{
    public OptionAlreadyExistsException(string message) : base(message)
    {
    }
}
