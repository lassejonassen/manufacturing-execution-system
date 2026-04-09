namespace WebAPI.Contracts.LineManagement.ProductionLines;

public sealed record UpdateProductionLineRequestDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
