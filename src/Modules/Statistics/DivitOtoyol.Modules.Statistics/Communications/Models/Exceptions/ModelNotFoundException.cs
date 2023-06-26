using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.Communications.Models.Exceptions;

public class ModelNotFoundException : NotFoundException
{
    public ModelNotFoundException(long id) : base($"Model with id {id} not found")
    {
    }
}
