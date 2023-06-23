namespace DivitOtoyol.Modules.Vehicles.Types.Dtos;

public record TypeDto
{
    public long Id { get; init; }
    public long ParentId { get; init; } = 1;
    public string Name { get; init; } = default!;
    public DateTime Created { get; init; }
}
