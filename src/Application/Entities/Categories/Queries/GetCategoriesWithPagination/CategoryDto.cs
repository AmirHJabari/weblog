using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;

namespace Weblog.Application.Queries.GetCategoriesWithPagination;

public class CategoryDto : IMapFrom<Category>
{
    public int Id { get; set; }

    /// <summary>
    /// The display name of the category.
    /// </summary>
    /// <value>Docker Desktop</value>
    public string Name { get; set; }
    
    /// <summary>
    /// The http url friendly name.
    /// </summary>
    /// <value>docker-desktop</value>
    public string UrlName { get; set; }

    public int BlogsCount { get; set; }
}
