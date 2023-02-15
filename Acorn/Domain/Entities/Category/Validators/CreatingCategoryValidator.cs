using FluentValidation;

namespace Acorn.Domain.Entities.Category;

public class CreatingCategoryValidator : AbstractValidator<Category>
{
    public CreatingCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.TagSetId).NotEmpty();
        RuleFor(x => x.LicenseId).NotEmpty();
    }
}