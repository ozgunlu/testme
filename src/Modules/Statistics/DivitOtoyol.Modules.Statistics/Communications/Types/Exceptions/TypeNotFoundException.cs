using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.Communications.Types.Exceptions;

public class TypeNotFoundException : NotFoundException
{
    public TypeNotFoundException(long id) : base($"Type with id {id} not found")
    {
    }
}
