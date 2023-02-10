namespace Acorn.Domain.Entities.Post;

public class PostFactory
{
    public async Task<Post> CreatePostAsync(string title, string body, Guid createdBy)
    {
        var post = new Post(title, body, createdBy);
        await DomainEvents.Raise(new CreatingPost(post));

        return post;
    }
}