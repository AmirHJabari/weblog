using FluentValidation;

namespace Weblog.Application.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(b => b.Name)
            .Length(1, 20)
            .NotEmpty();
    }
}
