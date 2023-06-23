namespace DivitOtoyol.Modules.Vehicles.Colors.Dtos;

public record ColorDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public DateTime Created { get; init; }
}
