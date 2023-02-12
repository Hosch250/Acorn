using FluentValidation;
using MediatR;

namespace Acorn.Domain.Entities.Tag;

public class CreatingTagValidationHandler : INotificationHandler<CreatingTag>
{
    private readonly CreatingTagValidator validator;

    public CreatingTagValidationHandler(CreatingTagValidator validator) => this.validator = validator;

    public async Task Handle(CreatingTag @event, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(@event.Entity);
    }
}