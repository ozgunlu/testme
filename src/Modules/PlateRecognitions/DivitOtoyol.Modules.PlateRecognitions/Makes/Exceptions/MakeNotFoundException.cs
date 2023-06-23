using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Makes.Exceptions;

public class MakeNotFoundException : NotFoundException
{
    public MakeNotFoundException(long id) : base($"Make with id {id} not found")
    {
    }
}
