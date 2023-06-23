using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Servers.Dtos;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Application;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using DivitOtoyol.Modules.Servers.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Servers.Servers.Features.GettingServerById;

public record GetServerById(long Id) : IQuery<GetServerByIdResponse>;

internal class GetServerByIdValidator : AbstractValidator<GetServerById>
{
    public GetServerByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetServerByIdHandler : IQueryHandler<GetServerById, GetServerByIdResponse>
{
    private readonly IServerDbContext _serverDbContext;
    private readonly IMapper _mapper;

    public GetServerByIdHandler(IServerDbContext serverDbContext, IMapper mapper)
    {
        _serverDbContext = serverDbContext;
        _mapper = mapper;
    }

    public async Task<GetServerByIdResponse> Handle(GetServerById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var server = await _serverDbContext.FindServerByIdAsync(command.Id);
        Guard.Against.NotFound(server, new ServerNotFoundException(command.Id));

        var serverDto = _mapper.Map<ServerDto>(server);

        return new GetServerByIdResponse(serverDto);
    }
}
