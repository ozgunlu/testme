using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Models.Dtos;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.GettingModels;

public record GetModels : ListQuery<GetModelsResponse>
{
}

internal class GetModelsValidator : AbstractValidator<GetModels>
{
    public GetModelsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetModelsHandler : IQueryHandler<GetModels, GetModelsResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetModelsHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetModelsResponse> Handle(
        GetModels command,
        CancellationToken cancellationToken)
    {
        var models = await _vehicleDbContext.Models
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(command.Includes)
            .ApplyFilter(command.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<Model, ModelDto>(_mapper.ConfigurationProvider, command.Page, command.PageSize, cancellationToken: cancellationToken);

        return new GetModelsResponse(models);
    }
}
