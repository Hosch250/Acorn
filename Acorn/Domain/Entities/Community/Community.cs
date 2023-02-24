using Microsoft.EntityFrameworkCore;

namespace Acorn.Domain.Entities.Community;

[Index(nameof(Host), IsUnique = true)]
public class Community : AggregateRoot
{
    /// <summary>
    /// Used for deserialization
    /// </summary>
    internal Community(Guid id, string name, string host, bool isFake, bool isHidden, Guid createdBy, DateTime createdOn)
    {
        Id = id;
        Name = name;
        Host = host;
        IsFake = isFake;
        IsHidden = isHidden;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    public Community(string name, string host, bool isFake, bool isHidden, Guid createdBy)
    {
        Id = Guid.Empty;
        Name = name;
        Host = host;
        IsFake = isFake;
        IsHidden = isHidden;
        SetCreatedBy(createdBy);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Host { get; private set; }
    public bool IsFake { get; private set; }
    public bool IsHidden { get; private set; }

    public Community WithName(string name)
    {
        Name = name;
        return this;
    }

    public Community WithHost(string host)
    {
        Host = host;
        return this;
    }

    public Community WithIsFake(bool isFake)
    {
        IsFake = isFake;
        return this;
    }

    public Community WithIsHidden(bool isHidden)
    {
        IsHidden = isHidden;
        return this;
    }
}