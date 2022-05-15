using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;

namespace Weblog.Application.Queries.GetBlogsWithPagination;

public class BlogBriefDto : IMapFrom<Blog>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string UrlTitle { get; set; }

    public string PosterImage { get; set; }
}
