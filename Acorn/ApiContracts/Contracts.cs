namespace Acorn.ApiContracts;

public record Post(Guid Id, string Title, string Body, int UpvoteCount, int DownvoteCount, List<string> Tags);
public record CreatePost(string Title, string Body, List<string> Tags);

public record Tag(Guid Id, string Name, string Description, string ShortDescription, string ParentId);
public record EditTag(Guid Id, string Name, string Description, string ShortDescription);