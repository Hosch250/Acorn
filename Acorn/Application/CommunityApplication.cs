using Acorn.Domain.Entities.Category;
using Acorn.Domain.Entities.Community;
using Acorn.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Application;

public interface ICommunityApplication
{
    Task<List<ApiContracts.Community>> GetAll();
    Task<ApiContracts.Community?> Get(Guid id);
    Task<ApiContracts.Community> Create(ApiContracts.CreateCommunity community);
    Task<ApiContracts.Community?> Edit(ApiContracts.Community community);
    Task Delete(Guid id);
}

public class CommunityApplication : ICommunityApplication
{
    private readonly IGlobalDbContext context;
    private readonly CommunityFactory communityFactory;
    private readonly IMapper mapper;

    public CommunityApplication(IGlobalDbContext context, CommunityFactory communityFactory, IMapper mapper)
    {
        this.context = context;
        this.communityFactory = communityFactory;
        this.mapper = mapper;
    }

    public async Task<List<ApiContracts.Community>> GetAll()
    {
        var communities = await context.Communities
            .OrderByDescending(o => o.Name)
            .ToListAsync();

        return communities.Select(mapper.Map<Community, ApiContracts.Community>).ToList();
    }

    public async Task<ApiContracts.Community?> Get(Guid id)
    {
        var community = await context.Communities.FindAsync(id);
        if (community is null)
        {
            return null;
        }

        return mapper.Map<Community, ApiContracts.Community>(community);
    }

    public async Task<ApiContracts.Community> Create(ApiContracts.CreateCommunity community)
    {
        // todo: get logged in user id
        var createdCommunity = await communityFactory.CreateCommunityAsync(community.Name, community.Host, community.IsFake, community.IsHidden, Guid.NewGuid());

        await context.Communities.AddAsync(createdCommunity);
        // todo: update site settings with new category order
        await context.SaveChangesAsync();

        return mapper.Map<Community, ApiContracts.Community>(createdCommunity);
    }

    public async Task<ApiContracts.Community?> Edit(ApiContracts.Community editCommunity)
    {
        // todo: track modified state
        var community = await context.Communities.FindAsync(editCommunity.Id);
        if (community is null)
        {
            return null;
        }

        var editedCommunity = community.WithName(editCommunity.Name)
            .WithHost(editCommunity.Host)
            .WithIsFake(editCommunity.IsFake)
            .WithIsHidden(editCommunity.IsHidden);

        await context.SaveChangesAsync();

        return mapper.Map<Community, ApiContracts.Community>(editedCommunity);
    }

    public async Task Delete(Guid id)
    {
        await context.Communities.Where(w => w.Id == id).ExecuteDeleteAsync();
    }
}