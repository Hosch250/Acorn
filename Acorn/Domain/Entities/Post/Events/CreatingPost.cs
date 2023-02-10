using MediatR;

namespace Acorn.Domain.Entities.Post;

public class CreatingPost : INotification
{
    public Post Entity { get; }

    public CreatingPost(Post entity) => Entity = entity;
}