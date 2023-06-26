using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;

public class CameraStatisticDomainException : DomainException
{
    public CameraStatisticDomainException(string message) : base(message)
    {
    }
}
