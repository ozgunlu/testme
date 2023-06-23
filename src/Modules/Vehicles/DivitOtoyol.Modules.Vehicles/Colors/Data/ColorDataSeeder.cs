using Bogus;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Colors.Data;

public class ColorDataSeeder : IDataSeeder
{
    private readonly IVehicleDbContext _context;

    public ColorDataSeeder(IVehicleDbContext context)
    {
        _context = context;
    }

    public async Task SeedAllAsync()
    {
        if (await _context.Colors.AnyAsync())
            return;

        long id = 1;

        var colorFaker = new Faker<Color>().CustomInstantiator(faker =>
        {
            var color = Color.Create(id, faker.Vehicle.Model());
            id++;
            return color;
        });
        var colors = colorFaker.Generate(5);

        await _context.Colors.AddRangeAsync(colors);
        await _context.SaveChangesAsync();
    }
}
