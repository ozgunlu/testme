namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption;

public record CreateOptionRequest(string Key, string Value, string Modules, string Description, bool CanUpdate, bool CanDelete);
