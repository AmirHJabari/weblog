using Weblog.Application.Commands.CreateBlog;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetBlogsWithPagination;

namespace Weblog.WebWasm.Interfaces;

public interface IBlogHttpClient
{
    Task<PaginatedList<BlogBriefDto>> GetWithPaginationAsync(GetBlogsWithPaginationQuery request, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CreateBlogCommand request, CancellationToken cancellationToken = default);
}