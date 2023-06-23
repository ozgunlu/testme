using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using Microsoft.EntityFrameworkCore;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Shared.Contracts;

public interface IVehicleDbContext
{
    DbSet<Color> Colors { get; }

    DbSet<VehicleType> VehicleTypes { get; }

    DbSet<Make> Makes { get; }

    DbSet<Model> Models { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
