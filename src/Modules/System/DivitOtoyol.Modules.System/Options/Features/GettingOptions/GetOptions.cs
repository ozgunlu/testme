using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptions;

public record GetOptions : ListQuery<GetOptionsResponse>
{
}

internal class GetOptionsValidator : AbstractValidator<GetOptions>
{
    public GetOptionsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetOptionsHandler : IQueryHandler<GetOptions, GetOptionsResponse>
{
    private readonly ISystemDbContext _systemDbContext;
    private readonly IMapper _mapper;

    public GetOptionsHandler(ISystemDbContext systemDbContext, IMapper mapper)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
    }

    public async Task<GetOptionsResponse> Handle(
        GetOptions request,
        CancellationToken cancellationToken)
    {
        var options = await _systemDbContext.Options
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Option, OptionDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetOptionsResponse(options);
    }
}
