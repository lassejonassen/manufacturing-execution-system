using SharedKernel.DomainEvents;

namespace SharedKernel;

public abstract class DomainEntity : BaseEntity
{
    public DomainEntity() : base() { }
    public DomainEntity(DateTimeOffset utcNow) : base(utcNow) { }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
