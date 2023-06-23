using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Systems.Options.Dtos;

namespace DivitOtoyol.Modules.Systems.Options.Features.GettingOptions;

public record GetOptionsResponse(ListResultModel<OptionDto> Options);
