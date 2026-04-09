namespace LineManagement.Application.ProductionLines.DTOs;

public sealed record CreateProductionLineDTO
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
