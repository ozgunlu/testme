using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using DivitOtoyol.Modules.Systems.Shared.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptionByKey;

public record GetOptionByKey(string Key) : IQuery<GetOptionByKeyResponse>;

internal class GetOptionByKeyValidator : AbstractValidator<GetOptionByKey>
{
    public GetOptionByKeyValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}

public class GetOptionByKeyHandler : IQueryHandler<GetOptionByKey, GetOptionByKeyResponse>
{
    private readonly ISystemDbContext _systemDbContext;
    private readonly IMapper _mapper;

    public GetOptionByKeyHandler(ISystemDbContext systemDbContext, IMapper mapper)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
    }

    public async Task<GetOptionByKeyResponse> Handle(GetOptionByKey command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var option = await _systemDbContext.Options.SingleOrDefaultAsync(o => o.Key == command.Key, cancellationToken);
        Guard.Against.NotFound(option, new OptionNotFoundException(command.Key));

        var optionDto = _mapper.Map<OptionDto>(option);

        return new GetOptionByKeyResponse(optionDto);
    }
}
