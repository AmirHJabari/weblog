namespace Weblog.Domain.Entities;

/// <summary>
/// This object represents a serie of blogs that discuss one topic.
/// </summary>
public class BlogsSerie : BaseEntity<string>
{
    public BlogsSerie()
        : base()
    {
        Blogs = new List<BlogsSerieItem>();
    }

    public string Title { get; set; }

    /// <summary>
    /// The http url friendly title.
    /// </summary>
    public string UrlTitle { get; set; }

    /// <summary>
    /// The blogs of this serie by order.
    /// </summary>
    public IList<BlogsSerieItem> Blogs { get; set; }
}

public class BlogsSerieItem
{
    /// <summary>
    /// The Id of the blog entity.
    /// </summary>
    public int Id { get; set; }

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
}