namespace SharedKernel;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedAtUtc { get; init; }
    public DateTimeOffset UpdatedAtUtc { get; set; }

    protected BaseEntity()
    {
        CreatedAtUtc = DateTimeOffset.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    protected BaseEntity(DateTimeOffset utcNow)
    {
        CreatedAtUtc = utcNow;
        UpdatedAtUtc = utcNow;
    }
}