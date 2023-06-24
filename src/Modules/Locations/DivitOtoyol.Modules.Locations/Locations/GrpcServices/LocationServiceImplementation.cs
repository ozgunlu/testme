using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using DivitOtoyol.Modules.Locations.Locations.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DivitOtoyol.Modules.Locations.Locations.GrpcServices;

public class LocationServiceImplementation : LocationServiceProto.LocationServiceProtoBase
{
    private readonly ILocationService _locationService;

    public LocationServiceImplementation(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public override async Task<LocationResponse> GetLocationById(LocationByIdRequest request, ServerCallContext context)
    {
        Guard.Against.Null(request, nameof(request));

        var location = await _locationService.GetLocationByIdAsync(request.Id) ?? throw new LocationNotFoundException(request.Id);

        return new LocationResponse
        {
            Id = location.Id,
            ParentId = location.ParentId,
            Name = location.Name,
            Created = Timestamp.FromDateTime(DateTime.SpecifyKind(location.Created, DateTimeKind.Utc)),
        };
    }
}
