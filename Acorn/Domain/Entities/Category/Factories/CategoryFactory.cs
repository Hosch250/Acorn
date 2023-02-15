using Acorn.Domain.Entities.SiteSettings;
using Acorn.Infrastructure;

namespace Acorn.Domain.Entities.Category;

public class CategoryFactory
{
    public async Task<Category> CreateCategoryAsync(string name, string description, string displayPostTypes, bool isHomepage, int minTrustLevel, bool useForHotPosts, bool useForAdvertisement, Guid tagSetId, LicenseEnum license, Guid createdBy)
    {
        var category = new Category(name, description, displayPostTypes, isHomepage, minTrustLevel, useForHotPosts, useForAdvertisement, tagSetId, license, createdBy);
        await DomainEvents.Raise(new CreatingCategory(category));

        return category;
    }
}