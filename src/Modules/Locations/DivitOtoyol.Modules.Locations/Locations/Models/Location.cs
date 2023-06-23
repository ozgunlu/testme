using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Domain;
using DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Domain;
using DivitOtoyol.Modules.Locations.Locations.Features.DeletingLocation;
using DivitOtoyol.Modules.Locations.Locations.ValueObjects;

namespace DivitOtoyol.Modules.Locations.Locations.Models;

public class Location : Aggregate<LocationId>
{
    public string Name { get; private set; }
    public LocationId ParentId { get; set; }
    public Location Parent { get; set; }
    public List<Location> Children { get; set; } = new List<Location>();

    public static Location Create(
        LocationId id,
        string name,
        LocationId parentId)
    {
        var location = new Location
        {
            Id = Guard.Against.Null(id, new LocationDomainException("Location id can not be null"))
        };

        location.ChangeName(name);
        location.SetParent(parentId);

        location.AddDomainEvents(new LocationCreated(location));

        return location;
    }

    /// <summary>
    /// Sets locations item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new LocationDomainException("Location name can not be null");

        Name = name;
    }

    /// <summary>
    /// Sets locations item parentId.
    /// </summary>
    /// <param name="parentId">The parentId to be changed.</param>
    public void SetParent(LocationId parentId)
    {
        ParentId = parentId;
    }

    public void AddChild(Location child)
    {
        if (child != null)
        {
            Children.Add(child);
            child.SetParent(Id);
        }
    }

    public void Delete()
    {
        AddDomainEvents(new LocationDeleted(this));
    }
}
