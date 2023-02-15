using Acorn.Domain.Entities.SiteSettings;

namespace Acorn.Domain.Entities.Category;

// note: make sure to add categoryOrder field as CSV to site settings
// that makes it WAY easier to adjust the category order in one spot, vs updating all other categories
public class Category : AggregateRoot
{
    /// <summary>
    /// Used for deserialization
    /// </summary>
    internal Category(
        Guid id,
        string name,
        string description,
        string displayPostTypes,
        bool isHomepage,
        int minTrustLevel,
        bool useForHotPosts,
        bool useForAdvertisement,
        Guid tagSetId,
        LicenseEnum licenseId,
        Guid createdBy,
        DateTime createdOn)
    {
        Id = id;
        Name = name;
        Description = description;
        DisplayPostTypes = displayPostTypes;
        IsHomepage = isHomepage;
        TagSetId = tagSetId;
        MinTrustLevel = minTrustLevel;
        LicenseId = licenseId;
        UseForHotPosts = useForHotPosts;
        UseForAdvertisement = useForAdvertisement;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    internal Category(
        string name,
        string description,
        string displayPostTypes,
        bool isHomepage,
        int minTrustLevel,
        bool useForHotPosts,
        bool useForAdvertisement,
        Guid tagSetId,
        LicenseEnum licenseId,
        Guid createdBy)
    {
        Id = Guid.Empty;
        Name = name;
        Description = description;
        DisplayPostTypes = displayPostTypes;
        IsHomepage = isHomepage;
        MinTrustLevel = minTrustLevel;
        UseForHotPosts = useForHotPosts;
        UseForAdvertisement = useForAdvertisement;
        TagSetId = tagSetId;
        LicenseId = licenseId;
        SetCreatedBy(createdBy);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string DisplayPostTypes { get; private set; }  // todo: figure out what this contains
    public bool IsHomepage { get; private set; }

    // todo: is this literally a min-reputation level?
    // if so, probably want to map that to a well-defined policy name, or something?
    public int MinTrustLevel { get; private set; }
    public bool UseForHotPosts { get; private set; }
    public bool UseForAdvertisement { get; private set; }
    public Guid TagSetId { get; private set; }
    public LicenseEnum LicenseId { get; private set; }

    // question: since these are all set at once, would it make more sense to
    // have one `WithValues` method? If we end up with other spots we set this,
    // we could pull the individual methods out later...
    public Category WithName(string name)
    {
        Name = name;
        return this;
    }

    public Category WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Category WithDisplayPostTypes(string displayPostTypes)
    {
        DisplayPostTypes = displayPostTypes;
        return this;
    }

    public Category WithIsHomepage(bool isHomepage)
    {
        IsHomepage = isHomepage;
        return this;
    }

    public Category WithMinTrustLevel(int minTrustLevel)
    {
        MinTrustLevel = minTrustLevel;
        return this;
    }

    public Category WithUseForHotPosts(bool useForHotPosts)
    {
        UseForHotPosts = useForHotPosts;
        return this;
    }

    public Category WithUseForAdvertisement(bool useForAdvertisement)
    {
        UseForAdvertisement = useForAdvertisement;
        return this;
    }

    public Category WithTagSetId(Guid tagSetId)
    {
        TagSetId = tagSetId;
        return this;
    }

    public Category WithLicenseId(LicenseEnum licenseId)
    {
        LicenseId = licenseId;
        return this;
    }

    public virtual TagSet TagSet { get; set; }
    public virtual License License { get; set; }
}