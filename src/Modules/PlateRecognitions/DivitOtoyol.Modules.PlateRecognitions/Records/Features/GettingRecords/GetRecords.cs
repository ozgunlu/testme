using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using BuildingBlocks.Core.Types;
using DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.GettingRecords;

public record GetRecords : ListQuery<GetRecordsResponse>;

public class GetRecordsValidator : AbstractValidator<GetRecords>
{
    public GetRecordsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetRecordsHandler : IQueryHandler<GetRecords, GetRecordsResponse>
{
    private readonly IPlateRecognitionDbContext _plateRecognitionDbContext;
    private readonly IMapper _mapper;

    public GetRecordsHandler(IPlateRecognitionDbContext plateRecognitionDbContext, IMapper mapper)
    {
        _plateRecognitionDbContext = plateRecognitionDbContext;
        _mapper = mapper;
    }

    public async Task<GetRecordsResponse> Handle(GetRecords request, CancellationToken cancellationToken)
    {
        var records = await _plateRecognitionDbContext.Records
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Record, RecordDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetRecordsResponse(records);
    }
}
