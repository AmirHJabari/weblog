namespace Weblog.Domain.Entities;

public class Category : AuditableEntity, IHasDomainEvent
{
    public Category()
    {
        Blogs = new List<Blog>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual IList<Blog> Blogs { get; set; }
    public List<DomainEvent> DomainEvents { get; set; }
}