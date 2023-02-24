namespace Acorn.Domain.Entities.Community;

public class CommunityFactory
{
    public async Task<Community> CreateCommunityAsync(string name, string host, bool isFake, bool isHidden, Guid createdBy)
    {
        var Community = new Community(name, host, isFake, isHidden, createdBy);
        await DomainEvents.Raise(new CreatingCommunity(Community));

        return Community;
    }
}