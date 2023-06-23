using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Data;
using FluentValidation;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption;

public record CreateOption(string Key, string Value, string Modules, string Description, bool CanUpdate, bool CanDelete)
    : ITxCreateCommand<CreateOptionResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateOptionValidator : AbstractValidator<CreateOption>
{
    private readonly SystemDbContext _systemDbContext;
    public CreateOptionValidator(SystemDbContext systemDbContext)
    {
        _systemDbContext = systemDbContext;

        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Key)
            .NotEmpty();

        RuleFor(x => x.Value)
            .NotEmpty();

        RuleFor(x => x.Modules)
            .NotEmpty();
    }
}

internal class CreateOptionHandler
    : ICommandHandler<CreateOption, CreateOptionResponse>
{
    private readonly SystemDbContext _systemDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOptionHandler> _logger;

    public CreateOptionHandler(
        SystemDbContext systemDbContext,
        IMapper mapper,
        ILogger<CreateOptionHandler> logger)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateOptionResponse> Handle(
        CreateOption command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (_systemDbContext.Options.Any(x => x.Key == command.Key))
            throw new OptionAlreadyExistsException($"Option with name '{command.Key}' already exists.");

        var option =
            Option.Create(
                command.Id,
                command.Key,
                command.Value,
                command.Modules,
                command.Description,
                command.CanUpdate,
                command.CanDelete);

        await _systemDbContext.AddAsync(option, cancellationToken);

        await _systemDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Option with id '{@Id}' saved successfully", option.Id);

        var optionDto = _mapper.Map<OptionDto>(option);

        return new CreateOptionResponse(optionDto);
    }
}
