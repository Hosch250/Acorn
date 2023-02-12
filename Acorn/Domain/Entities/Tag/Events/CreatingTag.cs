using MediatR;

namespace Acorn.Domain.Entities.Tag;

public class CreatingTag : INotification
{
    public Tag Entity { get; }

    public CreatingTag(Tag entity) => Entity = entity;
}