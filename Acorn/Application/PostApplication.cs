using Acorn.ApiContracts;
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

    // todo: update this to take skip values
    public async Task<List<ApiContracts.Post>> GetAll()
    {
        var posts = await context.Posts
            .Include(i => i.Votes)
            .OrderByDescending(o => o.CreatedOn)
            .Take(20)
            .ToListAsync();

        return posts.Select(mapper.Map<Domain.Entities.Post.Post, ApiContracts.Post>).ToList();
    }

    public async Task<ApiContracts.Post?> Get(Guid id)
    {
        var post = await context.Posts.Include(i => i.Votes).FirstOrDefaultAsync(f => f.Id == id);
        if (post is null)
        {
            return null;
        }

        return mapper.Map<Domain.Entities.Post.Post, ApiContracts.Post>(post);
    }

    public async Task<ApiContracts.Post> Create(CreatePost post)
    {
        // todo: get logged in user id
        var createdPost = await postFactory.CreatePostAsync(post.Title, post.Body, Guid.NewGuid());

        await context.Posts.AddAsync(createdPost);
        await context.SaveChangesAsync();

        return mapper.Map<Domain.Entities.Post.Post, ApiContracts.Post>(createdPost);
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
        await post.Upvote(Guid.NewGuid());
        await context.SaveChangesAsync();

        return mapper.Map<Domain.Entities.Post.Post, ApiContracts.Post>(post);
    }

    public async Task<ApiContracts.Post?> Downvote(Guid id)
    {
        var post = await context.Posts.FindAsync(id);
        if (post is null)
        {
            return null;
        }

        // todo: get logged in user id
        await post.Downvote(Guid.NewGuid());
        await context.SaveChangesAsync();

        return mapper.Map<Domain.Entities.Post.Post, ApiContracts.Post>(post);
    }
}