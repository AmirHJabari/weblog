namespace Weblog.Domain.Entities;

public class Tag : BaseEntity, IHasDomainEvent
{
    public Tag()
    {
        BlogRelations = new List<BlogToTagRelation>();
    }

    public string Name { get; set; }

    public virtual IList<BlogToTagRelation> BlogRelations { get; set; }
    public List<DomainEvent> DomainEvents { get; set; }
}