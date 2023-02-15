using Acorn.Domain.Entities.Category;
using Acorn.Domain.Entities.SiteSettings;
using Acorn.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Application;

public class CategoryApplication : ICategoryApplication
{
    private readonly IDbContext context;
    private readonly CategoryFactory categoryFactory;
    private readonly IMapper mapper;

    public CategoryApplication(IDbContext context, CategoryFactory categoryFactory, IMapper mapper)
    {
        this.context = context;
        this.categoryFactory = categoryFactory;
        this.mapper = mapper;
    }

    public async Task<List<ApiContracts.Category>> GetAll()
    {
        var tags = await context.Category
            //.OrderBy(o => o.Sequence)
            .ToListAsync();

        return tags.Select(mapper.Map<Category, ApiContracts.Category>).ToList();
    }

    public async Task<ApiContracts.Category?> Get(Guid id)
    {
        var category = await context.Category.FindAsync(id);
        if (category is null)
        {
            return null;
        }

        return mapper.Map<Category, ApiContracts.Category>(category);
    }

    public async Task<ApiContracts.Category> Create(ApiContracts.CreateCategory category)
    {
        // todo: get logged in user id
        var createdCategory = await categoryFactory.CreateCategoryAsync(category.Name, category.Description, category.DisplayPostTypes, category.IsHomepage, category.MinTrustLevel, category.UseForHotPosts, category.UseForAdvertisement, category.TagSetId, category.LicenseId, Guid.NewGuid());

        await context.Category.AddAsync(createdCategory);
        // todo: update site settings with new category order
        await context.SaveChangesAsync();

        return mapper.Map<Category, ApiContracts.Category>(createdCategory);
    }

    public async Task<ApiContracts.Category?> Edit(ApiContracts.Category editCategory)
    {
        // todo: track modified state
        var category = await context.Category.FindAsync(editCategory.Id);
        if (category is null)
        {
            return null;
        }

        var editedCategory = category
            .WithName(editCategory.Name)
            .WithDescription(editCategory.Description)
            .WithDisplayPostTypes(editCategory.DisplayPostTypes)
            .WithIsHomepage(editCategory.IsHomepage)
            .WithMinTrustLevel(editCategory.MinTrustLevel)
            .WithUseForHotPosts(editCategory.UseForHotPosts)
            .WithUseForAdvertisement(editCategory.UseForAdvertisement)
            .WithTagSetId(editCategory.TagSetId)
            .WithLicenseId(editCategory.LicenseId);

        // todo: update site settings with new category order
        await context.SaveChangesAsync();

        return mapper.Map<Category, ApiContracts.Category>(editedCategory);
    }

    public async Task Delete(Guid id)
    {
        await context.Category.Where(w => w.Id == id).ExecuteDeleteAsync();
    }
}