using Weblog.Application.Commands.CreateCategory;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetCategoriesWithPagination;

namespace Weblog.WebWasm.Interfaces;

public interface ICategoryHttpClient
{
    Task<PaginatedList<CategoryDto>> GetWithPaginationAsync(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken = default);
}