using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.GetBlog;

public class BlogDto : IMapFrom<Blog>
{
    public int Id { get; set; }

    public string CategoryName { get; set; }
    public string CategoryUrlName { get; set; }

    /// <summary>
    /// The title of the blog.
    /// </summary>
    /// <value>How To Work With Docker</value>
    public string Title { get; set; }

    /// <summary>
    /// The http url friendly title.
    /// </summary>
    /// <value>how-to-work-with-docker</value>
    public string UrlTitle { get; set; }

    /// <summary>
    /// Html content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Less then four lines description of <see cref="Content"/>
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Name of the poster image file.
    /// </summary>
    public string PosterImage { get; set; }

    public BlogStatus Status { get; set; }

    public TagDto[] Tags { get; set; }
}

public class TagDto : IMapFrom<Tag>
{
    public int Id { get; set; }
    public string Name { get; set; }
}