using Acorn.ApiContracts;

namespace Acorn.Application;
public interface IPostApplication
{
    Task<List<Post>> GetAll();
    Task<Post?> Get(Guid id);
    Task<Post> Create(CreatePost post);
    Task Delete(Guid id);
    Task<Post?> Upvote(Guid id);
    Task<Post?> Downvote(Guid id);
}
