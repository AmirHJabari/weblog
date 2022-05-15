using FluentValidation;

namespace Weblog.Application.Queries.GetBlogsWithPagination;

public class GetBlogsWithPaginationQueryValidator : AbstractValidator<GetBlogsWithPaginationQuery>
{
    public GetBlogsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");
    }
}
