namespace WebAPI.Contracts.LineManagement.Equipments;

public sealed record CreateEquipmentRequestDTO
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Guid ProductionLineId { get; init; }
}
