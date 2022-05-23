using FluentValidation;

namespace Weblog.Application.Queries.GetTagsWithPagination;

public class GetTagsWithPaginationQueryValidator : AbstractValidator<GetTagsWithPaginationQuery>
{
    public GetTagsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} at least greater than or equal to 1.");
    }
}
