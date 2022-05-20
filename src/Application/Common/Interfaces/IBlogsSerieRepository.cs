using Weblog.Domain.Entities;

namespace Weblog.Application.Common.Interfaces;

public interface IBlogsSerieRepository : IBaseRepository<BlogsSerie, string>
{
    Task<List<BlogsSerie>> GetManyByBlogIdAsync(int blogId, CancellationToken cancellationToken = default);
}