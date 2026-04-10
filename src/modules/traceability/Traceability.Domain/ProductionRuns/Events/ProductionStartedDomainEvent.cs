using SharedKernel.DomainEvents;

namespace Traceability.Domain.ProductionRuns.Events;

public sealed class ProductionStartedDomainEvent(Guid productionRunId) : DomainEvent
{
    public Guid ProductionRunId { get; } = productionRunId;
}
