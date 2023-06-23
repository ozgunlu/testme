using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using DivitOtoyol.Modules.Systems.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptionById;

public record GetOptionById(long Id) : IQuery<GetOptionByIdResponse>;

internal class GetOptionByIdValidator : AbstractValidator<GetOptionById>
{
    public GetOptionByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetOptionByIdHandler : IQueryHandler<GetOptionById, GetOptionByIdResponse>
{
    private readonly ISystemDbContext _systemDbContext;
    private readonly IMapper _mapper;

    public GetOptionByIdHandler(ISystemDbContext systemDbContext, IMapper mapper)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
    }

    public async Task<GetOptionByIdResponse> Handle(GetOptionById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var option = await _systemDbContext.FindOptionAsync(command.Id);
        Guard.Against.NotFound(option, new OptionNotFoundException(command.Id));

        var optionDto = _mapper.Map<OptionDto>(option);

        return new GetOptionByIdResponse(optionDto);
    }
}
