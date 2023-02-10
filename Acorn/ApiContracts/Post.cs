namespace Acorn.ApiContracts;

public record Post(Guid Id, string Title, string Body, int UpvoteCount, int DownvoteCount);
public record CreatePost(string Title, string Body);