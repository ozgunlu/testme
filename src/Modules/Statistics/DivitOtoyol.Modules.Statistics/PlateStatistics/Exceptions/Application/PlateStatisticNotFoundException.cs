using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Application;

public class PlateStatisticNotFoundException : NotFoundException
{
    public PlateStatisticNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public PlateStatisticNotFoundException(string message) : base(message)
    {
    }
}
