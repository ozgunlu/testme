using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Application;

public class CameraStatisticNotFoundException : NotFoundException
{
    public CameraStatisticNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public CameraStatisticNotFoundException(string message) : base(message)
    {
    }
}
