using System;
using System.Collections.Generic;
using System.Text;

namespace Traceability.Domain.ConsumedMaterials.Events;

public sealed class MaterialConsumedDomainEvent
{
    public DateTimeOffset Timestamp { get; set; }
    public Guid ProductionRunId { get; set; }
    public string Actor { get; set; } = string.Empty; // User or System
    public string EquipmentState { get; set; } = string.Empty; // Equipment State at SnapShot
}
