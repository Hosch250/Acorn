using Acorn.Infrastructure;
using Acorn.Domain.Entities.Post;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Application;

public class PostApplication : IPostApplication
{
    private readonly MySqlContext context;
    private readonly PostFactory postFactory;
    private readonly IMapper mapper;

    public PostApplication(MySqlContext context, PostFactory postFactory, IMapper mapper)
    {
        this.context = context;
        this.postFactory = postFactory;
        this.mapper = mapper;
    }

    public async Task<List<ApiContracts.Post>> GetAll(int skip = 0)
    {
        var posts = await context.Posts
            .Include(i => i.Tags)
            .Include(i => i.Votes)
            .OrderByDescending(o => o.CreatedOn)
            .Skip(skip)
            .Take(20)
            .ToListAsync();

        return posts.Select(mapper.Map<Post, ApiContracts.Post>).ToList();
    }

    public async Task<ApiContracts.Post?> Get(Guid id)
    {
        var post = await context.Posts
            .Include(i => i.Tags)
            .Include(i => i.Votes)
            .FirstOrDefaultAsync(f => f.Id == id);
        if (post is null)
        {
            return null;
        }

        return mapper.Map<Post, ApiContracts.Post>(post);
    }

    public async Task<ApiContracts.Post> Create(ApiContracts.CreatePost post)
    {
        // todo: get logged in user id
        var createdPost = await postFactory.CreatePostAsync(post.Title, post.Body, post.Tags, Guid.NewGuid());

        await context.Posts.AddAsync(createdPost);
        await context.SaveChangesAsync();

        return mapper.Map<Post, ApiContracts.Post>(createdPost);
    }

    public async Task Delete(Guid id)
    {
        await context.Posts.Where(w => w.Id == id).ExecuteDeleteAsync();
    }

    public async Task<ApiContracts.Post?> Upvote(Guid id)
    {
        var post = await context.Posts.FindAsync(id);
        if (post is null)
        {
            return null;
        }

        // todo: get logged in user id
        post.Upvote(Guid.NewGuid());
        await context.SaveChangesAsync();

        return mapper.Map<Post, ApiContracts.Post>(post);
    }

    public async Task<ApiContracts.Post?> Downvote(Guid id)
    {
        var post = await context.Posts.FindAsync(id);
        if (post is null)
        {
            return null;
        }

        // todo: get logged in user id
        post.Downvote(Guid.NewGuid());
        await context.SaveChangesAsync();

        return mapper.Map<Post, ApiContracts.Post>(post);
    }
}