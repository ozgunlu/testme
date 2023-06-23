using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Vehicles.Models.Exceptions.Domain;

public class ModelDomainException : DomainException
{
    public ModelDomainException(string message) : base(message)
    {
    }

    public ModelDomainException(long id) : base($"Category with id: '{id}' not found.")
    {
    }
}
