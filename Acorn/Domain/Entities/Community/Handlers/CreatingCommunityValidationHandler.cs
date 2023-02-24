using FluentValidation;
using MediatR;

namespace Acorn.Domain.Entities.Community;

public class CreatingCommunityValidationHandler : INotificationHandler<CreatingCommunity>
{
    private readonly CreatingCommunityValidator validator;

    public CreatingCommunityValidationHandler(CreatingCommunityValidator validator) => this.validator = validator;

    public async Task Handle(CreatingCommunity @event, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(@event.Entity, cancellationToken);
    }
}