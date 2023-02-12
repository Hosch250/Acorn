using Acorn.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Domain.Entities.Tag;

public class CreatingTagValidator : AbstractValidator<Tag>
{
    public CreatingTagValidator(MySqlContext context)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).CustomAsync(async (value, vContext, ct) =>
        {
            var tagNames = await context.Tags.Select(s => s.Name).ToListAsync(ct);
            if (tagNames.Contains(value))
            {
                vContext.AddFailure(nameof(Tag.Name), $"Tag '{value}' already exists");
            }
        });

        // todo: check user permissions
    }
}