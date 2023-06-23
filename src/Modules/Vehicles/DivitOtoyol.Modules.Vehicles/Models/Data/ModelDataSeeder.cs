using Bogus;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Models.Data;

public class ModelDataSeeder : IDataSeeder
{
    private readonly IVehicleDbContext _context;

    public ModelDataSeeder(IVehicleDbContext context)
    {
        _context = context;
    }

    public async Task SeedAllAsync()
    {
        if (await _context.Models.AnyAsync())
            return;

        /*long id = 1;

        // https://github.com/bchavez/Bogus
        // https://www.youtube.com/watch?v=T9pwE1GAr_U
        var modelFaker = new Faker<Model>().CustomInstantiator(faker =>
        {
            var model = Model.Create(id, faker.Vehicle.Model(), 1);
            id++;
            return model;
        });
        var models = modelFaker.Generate(5);

        await _context.Models.AddRangeAsync(models);
        await _context.SaveChangesAsync();*/
    }
}
