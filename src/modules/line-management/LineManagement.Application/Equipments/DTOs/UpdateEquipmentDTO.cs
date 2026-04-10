namespace LineManagement.Application.Equipments.DTOs;

public sealed record UpdateEquipmentDTO
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
}
