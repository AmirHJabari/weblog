using FluentValidation;

namespace Weblog.Application.Commands.CreateBlogsSerie;

public class CreateBlogsSerieCommandValidator : AbstractValidator<CreateBlogsSerieCommand>
{
    public CreateBlogsSerieCommandValidator()
    {
        RuleFor(b => b.Title)
            .Length(12, 100)
            .NotEmpty();
            
        RuleFor(b => b.UrlTitle)
            .Length(5, 150)
            .NotEmpty();
    }
}
