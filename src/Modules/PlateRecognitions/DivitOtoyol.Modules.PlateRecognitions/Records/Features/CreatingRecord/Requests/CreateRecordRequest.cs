namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;

public record CreateRecordRequest
{
    public string Plate { get; init; }
    public long CameraId { get; init; }
    //public string? OcrType { get; init; }
    public string? OcrMake { get; init; }
    public string? OcrModel { get; init; }
    public string? OcrColor { get; init; }
    public DateTime LprDate { get; init; }
    public string Metadata { get; init; }
    public string ImageData { get; init; }
}
