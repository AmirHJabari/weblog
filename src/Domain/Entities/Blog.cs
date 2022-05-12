namespace Weblog.Domain.Entities;

public class Blog : AuditableEntity
{
    public int Id { get; set; }
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
    /// Name of the poster image file.
    /// </summary>
    public string PosterImage { get; set; }

    /// <summary>
    /// Is available to public.
    /// </summary>
    public bool IsAvailable { get; set; }
    
    public virtual ICollection<BlogToTagRelation> TagsRelation { get; set; }
}