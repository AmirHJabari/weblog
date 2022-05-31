using FluentValidation;

namespace Weblog.Application.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(b => b.Name)
            .Length(1, 20).WithMessage("'{PropertyName}' must be between 1 and 20 characters.")
            .NotEmpty();
    }
}
