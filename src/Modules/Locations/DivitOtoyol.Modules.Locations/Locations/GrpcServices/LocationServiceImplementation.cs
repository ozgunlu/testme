using Ardalis.GuardClauses;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using DivitOtoyol.Modules.Locations.Locations.Protos;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using DivitOtoyol.Modules.Locations.Shared.Extensions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DivitOtoyol.Modules.Locations.Locations.GrpcServices;

public class LocationServiceImplementation : LocationService.LocationServiceBase
{
    private readonly IServiceProvider _serviceProvider;

    public LocationServiceImplementation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task<LocationResponse> GetLocationById(LocationByIdRequest request, ServerCallContext context)
    {
        Guard.Against.Null(request, nameof(request));

        using (var scope = _serviceProvider.CreateScope())
        {
            var locationDbContext = scope.ServiceProvider.GetRequiredService<ILocationDbContext>();

            var location = await locationDbContext.FindLocationAsync(request.Id) ?? throw new LocationNotFoundException(request.Id);

            return new LocationResponse
            {
                Id = location.Id,
                ParentId = location.ParentId,
                Name = location.Name,
                Created = Timestamp.FromDateTime(DateTime.SpecifyKind(location.Created, DateTimeKind.Utc)),
            };
        }
    }
}

