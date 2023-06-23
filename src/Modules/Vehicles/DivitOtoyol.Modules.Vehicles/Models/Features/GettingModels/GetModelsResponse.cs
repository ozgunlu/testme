using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Vehicles.Models.Dtos;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.GettingModels;

public record GetModelsResponse(ListResultModel<ModelDto> Models);
