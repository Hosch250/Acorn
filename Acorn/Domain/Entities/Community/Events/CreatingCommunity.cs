using MediatR;

namespace Acorn.Domain.Entities.Community;

public class CreatingCommunity : INotification
{
    public Community Entity { get; }

    public CreatingCommunity(Community entity) => Entity = entity;
}