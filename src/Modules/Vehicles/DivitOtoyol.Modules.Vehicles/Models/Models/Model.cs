using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Domain;
using DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelMake.Events;
using DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelType.Events;
using DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Models.Features.DeletingModel;
using DivitOtoyol.Modules.Vehicles.Models.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Models.Models;

public class Model : Aggregate<ModelId>
{
    public string Name { get; set; }
    public Make Make { get; set; }
    public MakeId MakeId { get; set; } = null!;
    public VehicleType Type { get; set; }
    public TypeId TypeId { get; set; } = null!;

    public static Model Create(
        ModelId id,
        string name,
        MakeId makeId,
        TypeId typeId)
    {
        var model = new Model
        {
            Id = Guard.Against.Null(id, new ModelDomainException("Model id can not be null"))
        };

        model.ChangeName(name);
        model.ChangeMake(makeId);
        model.ChangeType(typeId);

        model.AddDomainEvents(new ModelCreated(model));

        return model;
    }

    /// <summary>
    /// Sets models item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ModelDomainException("Model name can not be null.");

        Name = name;
    }

    public void ChangeMake(MakeId makeId)
    {
        Guard.Against.Null(makeId, new ModelDomainException("Make cannot be null"));

        // raising domain event immediately for checking some validation rule with some dependencies such as database
        DomainEventsInvoker.RaiseDomainEvent(new ChangingModelMake(makeId));

        MakeId = makeId;

        // add event to domain events list that will be raise during commiting transaction
        AddDomainEvents(new ModelMakeChanged(makeId, Id));
    }

    public void ChangeType(TypeId typeId)
    {
        Guard.Against.Null(typeId, new ModelDomainException("Type cannot be null"));

        // raising domain event immediately for checking some validation rule with some dependencies such as database
        DomainEventsInvoker.RaiseDomainEvent(new ChangingModelType(typeId));

        TypeId = typeId;

        // add event to domain events list that will be raise during commiting transaction
        AddDomainEvents(new ModelTypeChanged(typeId, Id));
    }

    public void Delete()
    {
        AddDomainEvents(new ModelDeleted(this));
    }
}
