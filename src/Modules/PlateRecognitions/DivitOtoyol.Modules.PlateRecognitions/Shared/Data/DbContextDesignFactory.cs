using BuildingBlocks.Persistence.EfCore.Postgres;
namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

public class PlateRecognitionDbContextDesignFactory : DbContextDesignFactoryBase<PlateRecognitionDbContext>
{
    public PlateRecognitionDbContextDesignFactory() : base("PlateRecognitions:PostgresOptions")
    {
    }
}
