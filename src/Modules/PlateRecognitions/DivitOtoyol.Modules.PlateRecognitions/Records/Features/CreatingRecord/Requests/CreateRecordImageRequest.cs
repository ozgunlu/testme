using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;

public record CreateRecordImageRequest
{
    public string BasePath { get; init; }
    public string SmallImageSize { get; init; }
    public string Plate { get; init; }
    public string PlateCountry { get; init; }
    public DateTime LprDate { get; init; }
    public string CameraName { get; init; }
    public string CameraBiosName { get; init; }
    public string? TypeName { get; init; }
    public string? MakeName { get; init; }
    public string? ModelName { get; init; }
    public string? ColorName { get; init; }
    public string ImageData { get; init; }
    public short? NRead { get; set; }
    public double? Speed { get; set; }
    public string? PlatePos { get; set; }
    public short? Score { get; set; }
    public bool IsNight { get; set; }
}
