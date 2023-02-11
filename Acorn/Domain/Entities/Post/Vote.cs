namespace Acorn.Domain.Entities.Post;

public enum VoteType
{
    Upvote = 1,
    Downvote = 2,
    Close = 3,
    Delete = 4
}

public record Vote(Guid Id, VoteType Type, Guid PostId, Guid UserId, DateTime CreatedOn, DateTime? RevokedOn = null)
{
    public static Vote Upvote(Guid postId, Guid userId) 
    {
        return new Vote(Guid.Empty, VoteType.Upvote, postId, userId, DateTime.UtcNow);
    }

    public static Vote Downvote(Guid postId, Guid userId)
    {
        return new Vote(Guid.Empty, VoteType.Downvote, postId, userId, DateTime.UtcNow);
    }
}