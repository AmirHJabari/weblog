using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;

namespace Weblog.Application.Queries.GetTagsWithPagination;

public class TagDto : IMapFrom<Tag>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int BlogRelationsCount { get; set; }
}
