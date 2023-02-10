using FluentValidation;

namespace Acorn.Domain.Entities.Post;

public class CreatingPostValidator : AbstractValidator<Post>
{
    public CreatingPostValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Body).NotEmpty();
    }
}