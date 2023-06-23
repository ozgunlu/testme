using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Domain;
using DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Makes.Features.DeletingMake;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Makes.Models;

public class Make : Aggregate<MakeId>
{
    public string Name { get; set; }

    public static Make Create(
        MakeId id,
        string name)
    {
        var make = new Make
        {
            Id = Guard.Against.Null(id, new MakeDomainException("Make id can not be null"))
        };

        make.ChangeName(name);

        make.AddDomainEvents(new MakeCreated(make));

        return make;
    }

    /// <summary>
    /// Sets types item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new MakeDomainException("Make name can not be null");

        Name = name;
    }

    /// <summary>
    /// Deletes the make by raising the "MakeDeleted" domain event.
    /// </summary>
    public void Delete()
    {
        AddDomainEvents(new MakeDeleted(this));
    }
}
