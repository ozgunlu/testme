using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

public class LocationStatisticDomainException : DomainException
{
    public LocationStatisticDomainException(string message) : base(message)
    {
    }
}
