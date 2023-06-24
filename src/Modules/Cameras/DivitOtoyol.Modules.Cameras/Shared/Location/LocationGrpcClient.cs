using DivitOtoyol.Modules.Locations.Locations.Protos;
using Grpc.Core;

namespace DivitOtoyol.Modules.Cameras.Shared.Location;

public class LocationGrpcClient : ILocationGrpcClient
{
    private readonly LocationServiceProto.LocationServiceProtoClient _client;

    public LocationGrpcClient(LocationServiceProto.LocationServiceProtoClient client)
    {
        _client = client;
    }

    public async Task<LocationResponse> GetLocationByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var request = new LocationByIdRequest { Id = id };

        try
        {
            return await _client.GetLocationByIdAsync(request, cancellationToken: cancellationToken);
        }
        catch (RpcException e) when (e.StatusCode == StatusCode.NotFound)
        {
            return null;
        }
    }
}
