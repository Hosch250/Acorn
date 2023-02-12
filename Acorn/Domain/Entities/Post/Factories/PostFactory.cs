using Acorn.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Domain.Entities.Post;

public class PostFactory
{
    private readonly MySqlContext context;

    public PostFactory(MySqlContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreatePostAsync(string title, string body, List<string> tags, Guid createdBy)
    {
        var dbTags = await context.Tags.Where(w => tags.Contains(w.Name)).ToListAsync();
        // todo: create tag if it doesn't exist (user perm check probably lives in tag creation logic)
        // question: if tag creation fails, should we continue creating the post without the tag
        //   or block creation until tag is removed?

        var post = new Post(title, body, dbTags, createdBy);
        await DomainEvents.Raise(new CreatingPost(post));

        return post;
    }
}