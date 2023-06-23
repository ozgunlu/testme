using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Domain;
using DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Domain;
using DivitOtoyol.Modules.Cameras.Cameras.Features.DeletingCamera;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;

namespace DivitOtoyol.Modules.Cameras.Cameras.Models;

public class Camera : Aggregate<CameraId>
{
    public LocationInformation LocationInformation { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? BiosName { get; set; }
    public string? SerialNumber { get; set; }
    public string? Ip { get; set; }

    public static Camera Create(
        CameraId id,
        LocationInformation locationInformation,
        string name,
        string? biosName,
        string? serialNumber,
        string? ip)
    {
        Guard.Against.Null(locationInformation, new CameraDomainException("LocationInformation cannot be null"));

        var camera = new Camera
        {
            Id = Guard.Against.Null(id, new CameraDomainException("Camera id can not be null")),
            LocationInformation = locationInformation
        };

        camera.ChangeName(name);
        camera.ChangeBiosName(biosName);
        camera.ChangeSerialNumber(serialNumber);
        camera.ChangeIp(ip);

        camera.AddDomainEvents(new CameraCreated(camera));

        return camera;
    }

    /// <summary>
    /// Updates the camera's location information.
    /// </summary>
    /// <param name="locationInformation">The new location information to be updated.</param>
    public void ChangeLocationInformation(LocationInformation locationInformation)
    {
        Guard.Against.Null(locationInformation, new CameraDomainException("LocationInformation cannot be null"));

        LocationInformation = locationInformation;
    }

    /// <summary>
    /// Sets cameras item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CameraDomainException("Camera name can not be null.");

        Name = name;
    }

    /// <summary>
    /// Sets location item bios_name.
    /// </summary>
    /// <param name="bios_name">The bios_name to be changed.</param>
    public void ChangeBiosName(string? bios_name)
    {
        BiosName = bios_name;
    }

    /// <summary>
    /// Sets location item serial_number.
    /// </summary>
    /// <param name="serial_number">The serial_number to be changed.</param>
    public void ChangeSerialNumber(string? serial_number)
    {
        SerialNumber = serial_number;
    }

    /// <summary>
    /// Sets location item ip.
    /// </summary>
    /// <param name="ip">The ip to be changed.</param>
    public void ChangeIp(string? ip)
    {
        Ip = ip;
    }

    public void Delete()
    {
        AddDomainEvents(new CameraDeleted(this));
    }
}
