using Weblog.Application.Commands.CreateTag;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetTagsWithPagination;

namespace Weblog.WebWasm.Interfaces;

public interface ITagHttpClient
{
    Task<PaginatedList<TagDto>> GetWithPaginationAsync(GetTagsWithPaginationQuery request, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CreateTagCommand request, CancellationToken cancellationToken = default);
}