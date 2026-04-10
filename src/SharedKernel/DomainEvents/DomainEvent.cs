using System.Diagnostics;

namespace SharedKernel.DomainEvents;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredAtUtc = DateTimeOffset.UtcNow;
        TraceId = Activity.Current?.Id;
    }
    protected DomainEvent(Guid id, DateTimeOffset occurredAtUtc, string traceId)
    {
        Id = id;
        OccurredAtUtc = occurredAtUtc;
        TraceId = traceId;
    }

    public Guid Id { get; init; }
    public DateTimeOffset OccurredAtUtc { get; init; }
    public string? TraceId { get; init; }
}
