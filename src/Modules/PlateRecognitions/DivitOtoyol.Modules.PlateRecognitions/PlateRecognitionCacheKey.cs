namespace DivitOtoyol.Modules.PlateRecognitions;

public static class PlateRecognitionCacheKey
{
    public static string ModelsByType(long typeId) => $"{nameof(ModelsByType)}{typeId}";
}
