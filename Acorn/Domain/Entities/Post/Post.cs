using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Acorn.Domain.Entities.Post;

public class Post : AggregateRoot
{
    /// <summary>
    /// Used for deserialization
    /// </summary>
    internal Post(Guid id, string title, string body, int upvoteCount, int downvoteCount, Guid createdBy, DateTime createdOn)
    {
        Id = id;
        Title = title;
        Body = body;
        UpvoteCount = upvoteCount;
        DownvoteCount = downvoteCount;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    public Post(string title, string body, Guid createdBy)
    {
        Title = title;
        Body = body;
        SetCreatedBy(createdBy);

        Id = Guid.NewGuid();
        UpvoteCount = 0;
        DownvoteCount = 0;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public int UpvoteCount { get; private set; }
    public int DownvoteCount { get; private set; }

    public async Task Upvote(Guid voteBy)
    {
        // validation happens in any event handler listening for this event
        // e.g. Does the user have the upvote permission, has the user already upvoted, etc
        // await DomainEvents.Raise(new UpvotePost(command));

        // todo: remove downvote if user previously downvoted

        UpvoteCount++;
    }

    public async Task Downvote(Guid voteBy)
    {
        // validation happens in any event handler listening for this event
        // e.g. Does the user have the upvote permission, has the user already downvoted, etc
        // await DomainEvents.Raise(new DownvotePost(command));

        // todo: remove upvote if user previously upvoted

        DownvoteCount++;
    }
}