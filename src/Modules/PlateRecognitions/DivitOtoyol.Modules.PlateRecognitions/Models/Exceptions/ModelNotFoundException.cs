using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Models.Exceptions;

public class ModelNotFoundException : NotFoundException
{
    public ModelNotFoundException(long id) : base($"Model with id {id} not found")
    {
    }
}
