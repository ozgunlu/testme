namespace DivitOtoyol.Modules.Vehicles.Makes.Dtos;

public record MakeDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public DateTime Created { get; init; }
}
