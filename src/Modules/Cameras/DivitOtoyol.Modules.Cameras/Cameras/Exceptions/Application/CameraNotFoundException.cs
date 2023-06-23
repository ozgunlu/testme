using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Application;

public class CameraNotFoundException : NotFoundException
{
    public CameraNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public CameraNotFoundException(string message) : base(message)
    {
    }
}
