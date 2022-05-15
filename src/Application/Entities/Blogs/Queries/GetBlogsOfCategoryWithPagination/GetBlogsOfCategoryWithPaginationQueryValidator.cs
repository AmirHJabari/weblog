using FluentValidation;

namespace Weblog.Application.Queries.GetBlogsOfCategoryWithPagination;

public class GetBlogsOfCategoryWithPaginationQueryValidator : AbstractValidator<GetBlogsOfCategoryWithPaginationQuery>
{
    public GetBlogsOfCategoryWithPaginationQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");
    }
}
