namespace Weblog.Domain.Entities;

public class Blog : AuditableEntity
{
    public Blog()
    {
        TagsRelation = new List<BlogToTagRelation>();
    }
    
    public int Id { get; set; }
    public int CategoryId { get; set; }

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

    public virtual IList<BlogToTagRelation> TagsRelation { get; set; }
    public virtual Category Category { get; set; }
}