namespace WebAPI.Contracts.LineManagement.ProductionLines;

public sealed record CreateProductionLineRequestDTO
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
