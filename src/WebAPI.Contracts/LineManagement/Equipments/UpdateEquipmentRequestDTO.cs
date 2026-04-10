namespace WebAPI.Contracts.LineManagement.Equipments;

public sealed record UpdateEquipmentRequestDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
