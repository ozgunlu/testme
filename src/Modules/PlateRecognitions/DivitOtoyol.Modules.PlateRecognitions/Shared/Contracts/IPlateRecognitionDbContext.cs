using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;

public interface IPlateRecognitionDbContext
{
    DbSet<Record> Records { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
