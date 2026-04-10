using SharedKernel.DomainEvents;

namespace Traceability.Domain.ConsumedMaterials.Events;

public sealed class MaterialConsumedDomainEvent(DateTimeOffset timeStamp, Guid consumedMaterialId, Guid productionRunId, string actor, string equipmentState) : DomainEvent
{
    public DateTimeOffset Timestamp { get; } = timeStamp;
    public Guid ConsumedMaterialId { get; } = consumedMaterialId;
    public Guid ProductionRunId { get; } = productionRunId;
    public string Actor { get; } = actor; // User or System
    public string EquipmentState { get; } = equipmentState; // Equipment State at SnapShot
}
