using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Domain;
using DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Colors.Features.DeletingColor;
using DivitOtoyol.Modules.Vehicles.Colors.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Colors.Models;

public class Color : Aggregate<ColorId>
{
    public string Name { get; set; } = default!;

    public static Color Create(
        ColorId id,
        string name)
    {
        var color = new Color
        {
            Id = Guard.Against.Null(id, new ColorDomainException("Color id can not be null"))
        };

        color.ChangeName(name);

        color.AddDomainEvents(new ColorCreated(color));

        return color;
    }

    /// <summary>
    /// Sets colors item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ColorDomainException("Color name can not be null");

        Name = name;
    }

    /// <summary>
    /// Deletes the color by raising the "ColorDeleted" domain event.
    /// </summary>
    public void Delete()
    {
        AddDomainEvents(new ColorDeleted(this));
    }
}
