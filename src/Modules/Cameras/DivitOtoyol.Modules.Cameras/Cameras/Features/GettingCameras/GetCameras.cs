using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using BuildingBlocks.Persistence.Mongo;
using DivitOtoyol.Modules.Cameras.Cameras.Dtos;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.GettingCameras;

public record GetCameras : ListQuery<GetCamerasResponse>
{
}

public class GetCamerasValidator : AbstractValidator<GetCameras>
{
    public GetCamerasValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetCamerasHandler : IQueryHandler<GetCameras, GetCamerasResponse>
{
    private readonly ICameraDbContext _cameraDbContext;
    private readonly IMapper _mapper;

    public GetCamerasHandler(ICameraDbContext cameraDbContext, IMapper mapper)
    {
        _cameraDbContext = cameraDbContext;
        _mapper = mapper;
    }

    public async Task<GetCamerasResponse> Handle(
        GetCameras request,
        CancellationToken cancellationToken)
    {
        var sortTuples = request.Sorts?.Select(sort => (sortBy: sort, sortOrder: "asc"));
        var cameras = await _cameraDbContext.Cameras
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .ApplySort(sortTuples)
            .AsNoTracking()
            .ApplyPagingAsync<Camera, CameraDto>(_mapper.ConfigurationProvider, request.Page, request.PageSize, cancellationToken: cancellationToken);

        return new GetCamerasResponse(cameras);
    }
}
