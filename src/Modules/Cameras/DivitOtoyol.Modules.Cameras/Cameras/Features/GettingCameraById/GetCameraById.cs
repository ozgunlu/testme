using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Cameras.Cameras.Dtos;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Application;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using DivitOtoyol.Modules.Cameras.Shared.Extensions;
using FluentValidation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.GettingCameraById;

public record GetCameraById(long Id) : IQuery<GetCameraByIdResponse>;

internal class GetCameraByIdValidator : AbstractValidator<GetCameraById>
{
    public GetCameraByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class GetCameraByIdHandler
    : IQueryHandler<GetCameraById, GetCameraByIdResponse>
{
    private readonly ICameraDbContext _cameraDbContext;
    private readonly IMapper _mapper;

    public GetCameraByIdHandler(ICameraDbContext cameraDbContext, IMapper mapper)
    {
        _cameraDbContext = cameraDbContext;
        _mapper = mapper;
    }

    public async Task<GetCameraByIdResponse> Handle(GetCameraById command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var camera = await _cameraDbContext.FindCameraAsync(command.Id);
        Guard.Against.NotFound(camera, new CameraNotFoundException(command.Id));

        var cameraDto = _mapper.Map<CameraDto>(camera);

        return new GetCameraByIdResponse(cameraDto);
    }
}
