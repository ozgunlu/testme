using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

public class PlateStatisticDomainException : DomainException
{
    public PlateStatisticDomainException(string message) : base(message)
    {
    }
}
