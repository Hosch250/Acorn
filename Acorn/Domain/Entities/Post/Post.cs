using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Acorn.Domain.Entities.Post;

public class Post : AggregateRoot
{
    /// <summary>
    /// Used for deserialization
    /// </summary>
    internal Post(Guid id, string title, string body, Guid createdBy, DateTime createdOn)
    {
        Id = id;
        Title = title;
        Body = body;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    public Post(string title, string body, Guid createdBy)
    {
        Title = title;
        Body = body;
        SetCreatedBy(createdBy);

        Id = Guid.Empty;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public int UpvoteCount { get => Votes.Count(w => w.Type == VoteType.Upvote && w.RevokedOn is null); }
    public int DownvoteCount { get => Votes.Count(w => w.Type == VoteType.Downvote && w.RevokedOn is null); }

    public virtual List<Vote> Votes { get; private set; } = new();

    public async Task Upvote(Guid voteBy)
    {
        // validation happens in any event handler listening for this event
        // e.g. Does the user have the upvote permission, etc
        // await DomainEvents.Raise(new UpvotePost(command));

        var activeVotes = Votes.Where(f => f.UserId == voteBy && f.RevokedOn is null).ToList();
        if (activeVotes.Any(a => a.Type == VoteType.Upvote))
        {
            return;
        }

        if (activeVotes.Any(a => a.Type == VoteType.Downvote))
        {
            var downvote = activeVotes.First(a => a.Type == VoteType.Downvote);
            downvote = downvote with { RevokedOn = DateTime.UtcNow };
        }

        Votes.Add(Vote.Upvote(Id, voteBy));
    }

    public async Task Downvote(Guid voteBy)
    {
        // validation happens in any event handler listening for this event
        // e.g. Does the user have the upvote permission, etc
        // await DomainEvents.Raise(new DownvotePost(command));

        var activeVotes = Votes.Where(f => f.UserId == voteBy && f.RevokedOn is null).ToList();
        if (activeVotes.Any(a => a.Type == VoteType.Downvote))
        {
            return;
        }

        if (activeVotes.Any(a => a.Type == VoteType.Upvote))
        {
            var upvote = activeVotes.First(a => a.Type == VoteType.Upvote);
            upvote = upvote with { RevokedOn = DateTime.UtcNow };
        }

        Votes.Add(Vote.Downvote(Id, voteBy));
    }
}