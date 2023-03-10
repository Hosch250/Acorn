using Acorn.Domain.Entities.Tag;
using Acorn.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Application;

public interface ITagApplication
{
    Task<List<ApiContracts.Tag>> GetAll(int skip = 0);
    Task<ApiContracts.Tag?> Get(Guid id);
    Task<ApiContracts.Tag?> Edit(ApiContracts.EditTag post);
    Task Delete(Guid id);
}

public class TagApplication : ITagApplication
{
    private readonly IDbContext context;
    private readonly IMapper mapper;

    public TagApplication(IDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<ApiContracts.Tag>> GetAll(int skip = 0)
    {
        var tags = await context.Tags
            .OrderByDescending(o => o.CreatedOn)
            .Skip(skip)
            .Take(100)
            .ToListAsync();

        return tags.Select(mapper.Map<Tag, ApiContracts.Tag>).ToList();
    }

    public async Task<ApiContracts.Tag?> Get(Guid id)
    {
        var tag = await context.Tags.FindAsync(id);
        if (tag is null)
        {
            return null;
        }

        return mapper.Map<Tag, ApiContracts.Tag>(tag);
    }

    public async Task<ApiContracts.Tag?> Edit(ApiContracts.EditTag editTag)
    {
        // todo: track modified state
        var tag = await context.Tags.FindAsync(editTag.Id);
        if (tag is null)
        {
            return null;
        }

        var editedTag = tag.WithName(editTag.Name)
            .WithDescription(editTag.Description)
            .WithShortDescription(editTag.ShortDescription);

        await context.SaveChangesAsync();

        return mapper.Map<Tag, ApiContracts.Tag>(editedTag);
    }

    public async Task Delete(Guid id)
    {
        await context.Tags.Where(w => w.Id == id).ExecuteDeleteAsync();
    }
}