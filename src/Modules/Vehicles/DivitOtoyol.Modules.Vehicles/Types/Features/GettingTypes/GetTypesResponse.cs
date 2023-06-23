using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Vehicles.Types.Dtos;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.GettingTypes;

public record GetTypesResponse(ListResultModel<TypeDto> Types);
