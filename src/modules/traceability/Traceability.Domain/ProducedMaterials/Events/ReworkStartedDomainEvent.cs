using SharedKernel.DomainEvents;

namespace Traceability.Domain.ProducedMaterials.Events;

public sealed class ReworkStartedDomainEvent(DateTimeOffset timeStamp, Guid producedMaterialId, Guid productionRunId, string actor, string equipmentState) : DomainEvent
{
    public DateTimeOffset Timestamp { get; } = timeStamp;
    public Guid ProducedMaterialId { get; } = producedMaterialId;
    public Guid ProductionRunId { get; } = productionRunId;
    public string Actor { get; } = actor; // User or System
    public string EquipmentState { get; } = equipmentState; // Equipment State at SnapShot
}
