using MediatR;

namespace Acorn.Domain.Entities.Category;

public class CreatingCategory : INotification
{
    public Category Entity { get; }

    public CreatingCategory(Category entity) => Entity = entity;
}