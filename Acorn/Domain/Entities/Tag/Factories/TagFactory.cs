using Acorn.Infrastructure;

namespace Acorn.Domain.Entities.Tag;

public class TagFactory
{
    public async Task<Tag> CreateTagAsync(string name, Guid tagSetId, Guid createdBy)
    {
        var Tag = new Tag(name, tagSetId, createdBy);
        await DomainEvents.Raise(new CreatingTag(Tag));

        return Tag;
    }
}