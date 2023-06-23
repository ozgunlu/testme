namespace DivitOtoyol.Modules.Servers.Servers.Dtos;

public record ServerDto
{
    public long Id { get; init; }
    public long LocationId { get; init; }
    public string LocationName { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Ip { get; init; } = default!;
}
