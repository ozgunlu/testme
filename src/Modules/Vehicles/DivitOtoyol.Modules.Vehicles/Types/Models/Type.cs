using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Domain;
using DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Types.Features.DeletingType;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Types.Models;

public class Type : Aggregate<TypeId>
{
    public string Name { get; set; }
    public TypeId ParentId { get; set; }
    public Type Parent { get; set; }
    public List<Type> Children { get; set; } = new List<Type>();

    public static Type Create(
        TypeId id,
        string name,
        TypeId parentId)
    {
        var type = new Type
        {
            Id = Guard.Against.Null(id, new TypeDomainException("Type id can not be null"))
        };

        type.ChangeName(name);
        type.SetParent(parentId);

        type.AddDomainEvents(new TypeCreated(type));

        return type;
    }

    /// <summary>
    /// Sets types item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new TypeDomainException("Type name can not be null");

        Name = name;
    }

    public void SetParent(TypeId parentId)
    {
        ParentId = parentId;
    }

    public void AddChild(Type child)
    {
        if (child != null)
        {
            Children.Add(child);
            child.SetParent(Id);
        }
    }

    public void Delete()
    {
        AddDomainEvents(new TypeDeleted(this));
    }
}
