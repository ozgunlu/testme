using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Application;

public class LocationStatisticNotFoundException : NotFoundException
{
    public LocationStatisticNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public LocationStatisticNotFoundException(string message) : base(message)
    {
    }
}
