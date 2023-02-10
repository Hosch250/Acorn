using FluentValidation;
using MediatR;

namespace Acorn.Domain.Entities.Post;

public class CreatingPostValidationHandler : INotificationHandler<CreatingPost>
{
    private readonly CreatingPostValidator validator;

    public CreatingPostValidationHandler(CreatingPostValidator validator) => this.validator = validator;

    public Task Handle(CreatingPost @event, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(@event.Entity);

        return Task.CompletedTask;
    }
}