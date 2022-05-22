using FluentValidation;

namespace Weblog.Application.Queries.GetBlog;

public class GetBlogQueryValidator : AbstractValidator<GetBlogQuery>
{
    public GetBlogQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}