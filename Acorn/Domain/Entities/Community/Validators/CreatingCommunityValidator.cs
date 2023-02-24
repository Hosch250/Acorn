using FluentValidation;

namespace Acorn.Domain.Entities.Community;

public class CreatingCommunityValidator : AbstractValidator<Community>
{
    public CreatingCommunityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Host).NotEmpty();
    }
}