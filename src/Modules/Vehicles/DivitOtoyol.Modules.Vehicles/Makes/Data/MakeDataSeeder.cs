using Bogus;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Makes.Data;

public class MakeDataSeeder : IDataSeeder
{
    private readonly IVehicleDbContext _context;

    public MakeDataSeeder(IVehicleDbContext context)
    {
        _context = context;
    }

    public async Task SeedAllAsync()
    {
        if (await _context.Makes.AnyAsync())
            return;

        var makes = await _context.Makes.ToListAsync();
        if (makes.Any())
        {
            _context.Makes.RemoveRange(makes);
            await _context.SaveChangesAsync();
        }

        makes = new List<Make>();

        // create values
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Acura"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Aion"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Alfa Romeo"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "AMC"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Anadol"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Aston Martin"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Audi"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Avanti"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Bentley"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "BMW"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Bugatti"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Buick"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Cadillac"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Chery"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Chevrolet"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Chrysler"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Dacia"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Daewoo"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Daihatsu"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Datsun"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "DeLorean"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Dodge"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "DS Automobiles"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Eagle"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Ferrari"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Fiat"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Fisker"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Ford"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Freightliner"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "GAZ"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Geely"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Geo"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "GMC"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Honda"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "HUMMER"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Hyundai"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Ikco"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Infiniti"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Isuzu"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Jaguar"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Jeep"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Kia"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lada"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lamborghini"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lancia"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Land Rover"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lexus"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lincoln"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Lotus"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Marcos"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Maserati"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Maybach"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Mazda"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "McLaren"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Mercedes-Benz"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Mercury"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Merkur"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "MG"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Mini"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Mitsubishi"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Moskwitsch"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Nissan"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Oldsmobile"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Opel"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Peugeot"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Plymouth"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Pontiac"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Porsche"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Proton"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "RAM"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Renault"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Rolls-Royce"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Rover"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Saab"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Saturn"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Scion"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Seat"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Skoda"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Smart"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "SRT"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Sterling"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Subaru"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Suzuki"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Tata"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Tesla"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Tofaş"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "TOGG"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Toyota"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Triumph"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Volkswagen"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Volvo"));
        makes.Add(Make.Create(SnowFlakIdGenerator.NewId(), "Yugo"));

        await _context.Makes.AddRangeAsync(makes);
        await _context.SaveChangesAsync();
    }
}
