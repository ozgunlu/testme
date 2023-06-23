using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Systems.Options.Exceptions.Domain;

public class OptionDomainException : DomainException
{
    public OptionDomainException(string message) : base(message)
    {
    }
}
