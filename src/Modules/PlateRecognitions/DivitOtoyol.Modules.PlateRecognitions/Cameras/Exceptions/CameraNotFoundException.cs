using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Cameras.Exceptions;

public class CameraNotFoundException : NotFoundException
{
    public CameraNotFoundException(long id) : base($"Camera with id {id} not found")
    {
    }
}
