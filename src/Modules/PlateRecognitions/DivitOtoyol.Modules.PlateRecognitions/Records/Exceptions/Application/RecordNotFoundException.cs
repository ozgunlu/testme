using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;

public class RecordNotFoundException : NotFoundException
{
    public RecordNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public RecordNotFoundException(string message) : base(message)
    {
    }
}
