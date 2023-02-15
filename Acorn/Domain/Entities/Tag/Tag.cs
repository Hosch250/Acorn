using Acorn.Domain.Entities.SiteSettings;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Acorn.Domain.Entities.Tag;

public class Tag : AggregateRoot
{
    /// <summary>
    /// Used for deserialization
    /// </summary>
    internal Tag(Guid id, string name, string description, string shortDescription, Guid tagSetId, Guid? parentId, Guid createdBy, DateTime createdOn)
    {
        Id = id;
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        TagSetId = tagSetId;
        ParentId = parentId;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    public Tag(string name, Guid tagSetId, Guid createdBy)
    {
        Id = Guid.Empty;
        Name = name;
        Description = string.Empty;
        ShortDescription = string.Empty;
        TagSetId = tagSetId;
        SetCreatedBy(createdBy);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ShortDescription { get; private set; }
    public Guid TagSetId { get; private set; }
    public Guid? ParentId { get; private set; }

    public Tag? Parent { get; }
    public TagSet TagSet { get; }
    public List<Post.Post> Posts { get; } = new();

    public Tag WithName(string name)
    {
        Name = name;
        return this;
    }

    public Tag WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Tag WithShortDescription(string shortDescription)
    {
        ShortDescription = shortDescription;
        return this;
    }
}