using Bogus;
using Bogus.Extensions.UnitedKingdom;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Locations.ValueObjects;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Locations.Data;

public class LocationDataSeeder : IDataSeeder
{
    private readonly ILocationDbContext _dbContext;

    public LocationDataSeeder(ILocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAllAsync()
    {
        if (await _dbContext.Locations.AnyAsync())
            return;

        var locations = new List<Location>();

        // create root
        var root = Location.Create(new LocationId(1), "Root", 1);

        locations.Add(root);

        // create root types
        var itu = Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "İTÜ", root.Id);

        locations.Add(itu);

        // create child types
        var ayazaga = Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Ayazağa", itu.Id);
        itu.AddChild(ayazaga);
        ayazaga.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Enerji Çıkış", ayazaga.Id));
        ayazaga.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Enerji Giriş", ayazaga.Id));
        ayazaga.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Etiler", ayazaga.Id));
        ayazaga.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Borsa", ayazaga.Id));
        ayazaga.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Akademi", ayazaga.Id));

        var macka = Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Maçka", itu.Id);
        itu.AddChild(macka);
        macka.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Maçka-1 Çıkış", macka.Id));
        macka.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Maçka-1 Giriş", macka.Id));
        macka.AddChild(Location.Create(new LocationId(SnowFlakIdGenerator.NewId()), "Maçka-2", macka.Id));

        await _dbContext.Locations.AddRangeAsync(locations);
        await _dbContext.SaveChangesAsync();
    }
}
