using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using Traceability.Domain.ProductionRuns.Enums;

namespace Traceability.Domain.ProductionRuns.Entities;

public sealed class ProductionRun : BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid WorkOrderId {  get; private set; }
    public Guid OperationId { get; private set; }
    public Guid EquipmentId { get; private set; }
    public Guid ProductionLineId { get; private set; }
    public DateTimeOffset StartTimeUtc { get; private set; }
    public DateTime EndTimeUtc { get; private set; }
    public ProductionRunState State { get; private set; }
    public List<string> OperatorIds { get; private set; } = [];
    public Guid ShiftId { get; private set; }
}
