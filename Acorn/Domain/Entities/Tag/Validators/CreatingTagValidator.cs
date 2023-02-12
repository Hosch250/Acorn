using FluentValidation;

namespace Acorn.Domain.Entities.Tag;

public class CreatingTagValidator : AbstractValidator<Tag>
{
    public CreatingTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}