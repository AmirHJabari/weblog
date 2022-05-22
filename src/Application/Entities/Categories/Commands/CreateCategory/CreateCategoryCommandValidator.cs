using FluentValidation;

namespace Weblog.Application.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(b => b.Name)
            .Length(1, 20)
            .NotEmpty();
            
        RuleFor(b => b.UrlName)
            .Length(1, 30)
            .NotEmpty();
            
        RuleFor(b => b.Description)
            .Length(10, 400)
            .NotEmpty();
    }
}
