namespace DivitOtoyol.Modules.Vehicles;

public static class VehicleCacheKey
{
    public static string ModelsByType(long typeId) => $"{nameof(ModelsByType)}{typeId}";
}
