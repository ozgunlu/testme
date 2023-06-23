using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecords;

public record GetRecordsResponse(ListResultModel<RecordDto> Records);
