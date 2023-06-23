using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Vehicles.Makes.Dtos;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.GettingMakes;

public record GetMakesResponse(ListResultModel<MakeDto> Makes);
