namespace LineManagement.Application.Equipments.DTOs;

public sealed record CreateEquipmentDTO
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Guid ProductionLineId { get; init; }
}
