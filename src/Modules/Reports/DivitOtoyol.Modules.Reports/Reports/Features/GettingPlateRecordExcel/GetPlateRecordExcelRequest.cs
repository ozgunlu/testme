using DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record.Dtos;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordExcel;

public class GetPlateRecordExcelRequest
{
    public RecordDto Filter { get; set; }
    public int Type { get; set; }
}
