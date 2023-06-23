using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Vehicles.Colors.Dtos;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.GettingColors;

public record GetColorsResponse(ListResultModel<ColorDto> Colors);
