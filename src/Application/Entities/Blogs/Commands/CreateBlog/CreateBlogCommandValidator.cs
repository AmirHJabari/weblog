using FluentValidation;

namespace Weblog.Application.Commands.CreateBlog;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(b => b.Title)
            .Length(12, 100)
            .NotEmpty();
            
        RuleFor(b => b.UrlTitle)
            .Length(5, 150)
            .NotEmpty();
            
        RuleFor(b => b.Content)
            .Length(10, 10000)
            .NotEmpty();
            
        RuleFor(b => b.Summary)
            .Length(10, 400)
            .NotEmpty();

        RuleFor(b => b.PosterImage)
            .Length(20, 100)
            .NotEmpty();

        RuleFor(b => b.CategoryId)
            .GreaterThanOrEqualTo(1);

        RuleFor(b => b.TagsIds)
            .NotEmpty()
            .WithName("Tags")
            .WithErrorCode("NoTagSelected");
        RuleFor(b => b.TagsIds.Count)
            .Must(c => c < 8)
            .WithErrorCode("TooManyTags");

        RuleFor(b => b.Status).IsInEnum();
    }
}
