using BuildingBlocks.Core.Exception.Types;

namespace ECommerce.Modules.Customers.Customers.Exceptions.Domain;

public class InvalidNationalityException : BadRequestException
{
    public string Nationality { get; }

    public InvalidNationalityException(string nationality) : base($"Nationality: '{nationality}' is invalid.")
    {
        Nationality = nationality;
    }
}
