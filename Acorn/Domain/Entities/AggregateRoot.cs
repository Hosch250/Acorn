namespace Acorn.Domain.Entities;

public abstract class AggregateRoot
{
    public Guid CreatedBy { get; protected set; }
    public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;

    public void SetCreatedBy(Guid createdBy)
    {
        CreatedBy = createdBy;
    }
}