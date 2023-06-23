using DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record.Dtos;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordZip;

public class GetPlateRecordZipRequest
{
    public RecordDto Filter { get; set; }
    public int Type { get; set; }
}
