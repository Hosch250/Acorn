using Acorn.Domain.Entities.SiteSettings;

namespace Acorn.ApiContracts;

public record Post(Guid Id, string Title, string Body, int UpvoteCount, int DownvoteCount, List<string> Tags);
public record CreatePost(string Title, string Body, List<string> Tags);

public record Tag(Guid Id, string Name, string Description, string ShortDescription, Guid? ParentId);
public record EditTag(Guid Id, string Name, string Description, string ShortDescription);

public record Category(
        Guid Id,
        string Name,
        string Description,
        string DisplayPostTypes,
        bool IsHomepage,
        int MinTrustLevel,
        bool UseForHotPosts,
        bool UseForAdvertisement,
        Guid TagSetId,
        LicenseEnum LicenseId);

public record CreateCategory(
        string Name,
        string Description,
        string DisplayPostTypes,
        bool IsHomepage,
        int MinTrustLevel,
        int Sequence,
        bool UseForHotPosts,
        bool UseForAdvertisement,
        Guid TagSetId,
        LicenseEnum LicenseId);