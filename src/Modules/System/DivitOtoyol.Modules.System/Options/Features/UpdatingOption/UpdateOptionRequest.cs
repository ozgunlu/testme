namespace DivitOtoyol.Modules.Systems.Options.Features.UpdatingOption;

public record UpdateOptionRequest(string Key, string Value, string Modules, bool CanUpdate, bool CanDelete);
