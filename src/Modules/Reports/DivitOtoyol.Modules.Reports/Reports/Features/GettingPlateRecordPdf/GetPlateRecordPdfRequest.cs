using DivitOtoyol.Modules.Reports.Shared.PlateRecognition.Record.Dtos;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordPdf;

public class GetPlateRecordPdfRequest
{
    public RecordDto Filter { get; set; }
    public int Type { get; set; }
}
