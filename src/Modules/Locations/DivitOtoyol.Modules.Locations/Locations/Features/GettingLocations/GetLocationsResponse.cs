using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Locations.Locations.Dtos;

namespace DivitOtoyol.Modules.Locations.Locations.Features.GettingLocations;

public record GetLocationsResponse(ListResultModel<LocationDto> Locations);
