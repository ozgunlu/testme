using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.Communications.Cameras.Exceptions;

public class CameraNotFoundException : NotFoundException
{
    public CameraNotFoundException(long id) : base($"Camera with id {id} not found")
    {
    }
}
