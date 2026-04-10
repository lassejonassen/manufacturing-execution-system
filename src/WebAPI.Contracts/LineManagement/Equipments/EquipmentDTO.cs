namespace WebAPI.Contracts.LineManagement.Equipments;

public sealed record EquipmentDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Guid ProductionLineId { get; init; }
}
