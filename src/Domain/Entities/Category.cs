namespace Weblog.Domain.Entities;

public class Category : BaseEntity, IHasDomainEvent
{
    public Category() : base()
    {
        Blogs = new List<Blog>();
    }

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

    public string Description { get; set; }

    public virtual IList<Blog> Blogs { get; set; }
    public List<DomainEvent> DomainEvents { get; set; }
}