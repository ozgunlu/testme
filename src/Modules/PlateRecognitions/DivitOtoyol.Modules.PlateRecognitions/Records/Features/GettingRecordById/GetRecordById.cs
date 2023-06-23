using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecordById;

public record GetRecordById(long Id) : IQuery<GetRecordByIdResponse>;

internal class GetRecordByIdValidator : AbstractValidator<GetRecordById>
{
    public GetRecordByIdValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetRecordByIdHandler : IQueryHandler<GetRecordById, GetRecordByIdResponse>
{
    private readonly IPlateRecognitionDbContext _plateRecognitionDbContext;
    private readonly IMapper _mapper;

    public GetRecordByIdHandler(IPlateRecognitionDbContext plateRecognitionDbContext, IMapper mapper)
    {
        _plateRecognitionDbContext = plateRecognitionDbContext;
        _mapper = mapper;
    }

    public async Task<GetRecordByIdResponse> Handle(GetRecordById query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var record = await _plateRecognitionDbContext.FindRecordAsync(query.Id);
        Guard.Against.NotFound(record, new RecordNotFoundException(query.Id));

        var recordDto = _mapper.Map<RecordDto>(record);

        return new GetRecordByIdResponse(recordDto);
    }
}

public record GetRecordByIdResponse(RecordDto Record);
