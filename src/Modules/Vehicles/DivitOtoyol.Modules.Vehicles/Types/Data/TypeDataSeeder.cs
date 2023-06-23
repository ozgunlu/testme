using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;
using Microsoft.EntityFrameworkCore;

using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Data
{
    public class TypeDataSeeder : IDataSeeder
    {
        private readonly IVehicleDbContext _context;

        public TypeDataSeeder(IVehicleDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            var types = await _context.VehicleTypes.ToListAsync();
            if (types.Any())
            {
                _context.VehicleTypes.RemoveRange(types);
                await _context.SaveChangesAsync();
            }

            types = new List<VehicleType>();

            // create root
            var root = VehicleType.Create(new TypeId(1), "Root", 1);
            types.Add(root);

            // create others
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Otomobil", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Arazi, SUV & Pickup", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Motosiklet", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Minivan & Panelvan", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Minibüs & Midibüs", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Otobüs", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Kamyon & Kamyonet", 1));
            types.Add(VehicleType.Create(SnowFlakIdGenerator.NewId(), "Çekici", 1));

            await _context.VehicleTypes.AddRangeAsync(types);
            await _context.SaveChangesAsync();
        }
    }
}
