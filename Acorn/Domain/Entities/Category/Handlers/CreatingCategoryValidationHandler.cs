using FluentValidation;
using MediatR;

namespace Acorn.Domain.Entities.Category;

public class CreatingCategoryValidationHandler : INotificationHandler<CreatingCategory>
{
    private readonly CreatingCategoryValidator validator;

    public CreatingCategoryValidationHandler(CreatingCategoryValidator validator) => this.validator = validator;

    public async Task Handle(CreatingCategory @event, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(@event.Entity, cancellationToken);
    }
}