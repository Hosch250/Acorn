using Acorn.Domain.Entities.Tag;
using Acorn.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Domain.Entities.Post;

public class PostFactory
{
    private readonly MySqlContext context;
    private readonly TagFactory tagFactory;

    public PostFactory(MySqlContext context, TagFactory tagFactory)
    {
        this.context = context;
        this.tagFactory = tagFactory;
    }

    public async Task<Post> CreatePostAsync(string title, string body, List<string> tags, Guid createdBy)
    {
        tags = tags.Distinct().ToList();

        var dbTags = await context.Tags.Where(w => tags.Contains(w.Name)).ToListAsync();
        foreach (var tag in tags.Where(w => !dbTags.Any(t => t.Name == w)))
        {
            // todo: get tagset id and user id
            var createdTag = await tagFactory.CreateTagAsync(tag, Guid.NewGuid(), Guid.NewGuid());

            context.Tags.Add(createdTag);
            dbTags.Add(createdTag);
        }

        // question: if tag creation fails, should we continue creating the post without the tag
        //   or block creation until tag is removed?

        var post = new Post(title, body, dbTags, createdBy);
        await DomainEvents.Raise(new CreatingPost(post));

        return post;
    }
}