using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Domain;
using DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Domain;
using DivitOtoyol.Modules.Systems.Options.Features.DeletingOption;
using DivitOtoyol.Modules.Systems.Options.ValueObjects;

namespace DivitOtoyol.Modules.Systems.Options.Models;

public class Option : Aggregate<OptionId>
{
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string Modules { get; private set; }
    public string Description { get; private set; }
    public bool CanUpdate { get; private set; }
    public bool CanDelete { get; private set; }

    public static Option Create(
        OptionId id,
        string key,
        string value,
        string modules,
        string description,
        bool canUpdate,
        bool canDelete)
    {
        var option = new Option
        {
            Id = Guard.Against.Null(id, new OptionDomainException("Option id can not be null"))
        };

        option.ChangeKey(key);
        option.ChangeValue(value);
        option.ChangeModules(modules);
        option.ChangeDescription(description);
        option.ChangeCanUpdate(canUpdate);
        option.ChangeCanDelete(canDelete);

        option.AddDomainEvents(new OptionCreated(option));

        return option;
    }

    /// <summary>
    /// Sets options item key.
    /// </summary>
    /// <param name="key">The key to be changed.</param>
    public void ChangeKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new OptionDomainException("Option key can not be null");

        Key = key;
    }

    /// <summary>
    /// Sets options item value.
    /// </summary>
    /// <param name="value">The value to be changed.</param>
    public void ChangeValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new OptionDomainException("Option value can not be null");

        Value = value;
    }

    /// <summary>
    /// Sets options item modules.
    /// </summary>
    /// <param name="modules">The modules to be changed.</param>
    public void ChangeModules(string modules)
    {
        if (string.IsNullOrWhiteSpace(modules))
            throw new OptionDomainException("Option modules can not be null");

        Modules = modules;
    }

    /// <summary>
    /// Sets options item description.
    /// </summary>
    /// <param name="description">The description to be changed.</param>
    public void ChangeDescription(string description)
    {
        Description = description;
    }

    /// <summary>
    /// Sets if option can be updated.
    /// </summary>
    /// <param name="canUpdate">Value indicating whether the option can be updated.</param>
    public void ChangeCanUpdate(bool canUpdate)
    {
        CanUpdate = canUpdate;
    }

    /// <summary>
    /// Sets if option can be deleted.
    /// </summary>
    /// <param name="canDelete">Value indicating whether the option can be deleted.</param>
    public void ChangeCanDelete(bool canDelete)
    {
        CanDelete = canDelete;
    }

    public void Delete()
    {
        AddDomainEvents(new OptionDeleted(this));
    }
}
