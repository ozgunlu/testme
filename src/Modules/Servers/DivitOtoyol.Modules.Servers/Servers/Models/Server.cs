using Ardalis.GuardClauses;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Domain;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Domain;
using DivitOtoyol.Modules.Servers.Servers.Features.DeletingServer;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;

namespace DivitOtoyol.Modules.Servers.Servers.Models;

public class Server : Aggregate<ServerId>
{
    public LocationInformation LocationInformation { get; private set; } = default!;
    public Name Name { get; private set; } = default!;
    public string Ip { get; private set; } = default!;

    public static Server Create(
        ServerId id,
        LocationInformation locationInformation,
        Name name,
        string ip)
    {
        Guard.Against.Null(locationInformation, new ServerDomainException("LocationInformation cannot be null"));

        var server = new Server
        {
            Id = Guard.Against.Null(id, new ServerDomainException("Server id can not be null")),
            LocationInformation = locationInformation
        };

        server.ChangeName(name);
        server.ChangeIp(ip);

        server.AddDomainEvents(new ServerCreated(server));

        return server;
    }

    /// <summary>
    /// Updates the server's location information.
    /// </summary>
    /// <param name="locationInformation">The new location information to be updated.</param>
    public void ChangeLocationInformation(LocationInformation locationInformation)
    {
        Guard.Against.Null(locationInformation, new ServerDomainException("LocationInformation cannot be null"));

        LocationInformation = locationInformation;
    }

    /// <summary>
    /// Sets location item name.
    /// </summary>
    /// <param name="name">The name to be changed.</param>
    public void ChangeName(ValueObjects.Name name)
    {
        Guard.Against.Null(name, new ServerDomainException("Server name cannot be null."));

        Name = name;
    }

    /// <summary>
    /// Sets location item ip.
    /// </summary>
    /// <param name="ip">The ip to be changed.</param>
    public void ChangeIp(string ip)
    {
        Guard.Against.Null(ip, new ServerDomainException("Ip cannot be null."));

        if (Ip == ip)
            return;

        Ip = ip;
    }

    public void Delete()
    {
        AddDomainEvents(new ServerDeleted(this));
    }
}
