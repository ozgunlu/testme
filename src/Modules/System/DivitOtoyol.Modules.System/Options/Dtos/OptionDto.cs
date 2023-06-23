namespace DivitOtoyol.Modules.Systems.Options.Dtos;

public record OptionDto
{
    public long Id { get; init; }
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string Modules { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
    public DateTime Created { get; init; }
}
