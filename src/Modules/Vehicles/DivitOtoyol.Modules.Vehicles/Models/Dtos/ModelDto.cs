namespace DivitOtoyol.Modules.Vehicles.Models.Dtos;

public record ModelDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public long MakeId { get; init; }
    public string MakeName { get; init; } = default!;
    public long TypeId { get; init; }
    public string TypeName { get; init; } = default!;
    public DateTime Created { get; init; }
}
