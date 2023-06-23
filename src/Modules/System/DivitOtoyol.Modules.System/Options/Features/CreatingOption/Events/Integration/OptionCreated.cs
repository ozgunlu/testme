using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Integration;

public record OptionCreated(long Id, string Key, string Value, string Modules, bool CanUpdate, bool CanDelete) : IntegrationEvent;
