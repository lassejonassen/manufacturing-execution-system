namespace LineManagement.Application.ProductionLines.DTOs;

public sealed record ProductionLineDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
