using Bogus;
using Bogus.Extensions.UnitedKingdom;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Cameras.Data;

public class CameraDataSeeder : IDataSeeder
{
    private readonly ICameraDbContext _dbContext;

    public CameraDataSeeder(ICameraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAllAsync()
    {
        if (await _dbContext.Cameras.AnyAsync())
            return;

        /*long id = 1;

        // https://github.com/bchavez/Bogus
        // https://www.youtube.com/watch?v=T9pwE1GAr_U
        var cameraFaker = new Faker<Camera>().CustomInstantiator(faker =>
        {
            var camera = Camera.Create(
                id,
                faker.Commerce.ProductName(),
                faker.Random.Long(1, 3),
                faker.Commerce.ProductName(),
                faker.Commerce.ProductName(),
                faker.Commerce.ProductName());
            id++;

            return camera;
        });
        var cameras = cameraFaker.Generate(10);

        try
        {
            await _dbContext.Cameras.AddRangeAsync(cameras);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }*/
    }
}
