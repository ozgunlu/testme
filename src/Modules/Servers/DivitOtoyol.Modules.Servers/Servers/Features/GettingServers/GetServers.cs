using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Servers.Servers.Dtos;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Servers.Features.GettingServers;

public record GetServers : ListQuery<GetServersResponse>;

public class GetServersValidator : AbstractValidator<GetServers>
{
    public GetServersValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetServersHandler : IQueryHandler<GetServers, GetServersResponse>
{
    private readonly IServerDbContext _cameraDbContext;
    private readonly IMapper _mapper;

    public GetServersHandler(IServerDbContext cameraDbContext, IMapper mapper)
    {
        _cameraDbContext = cameraDbContext;
        _mapper = mapper;
    }

    public async Task<GetServersResponse> Handle(GetServers request, CancellationToken cancellationToken)
    {
        var cameras = await _cameraDbContext.Servers
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Server, ServerDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetServersResponse(cameras);
    }
}
