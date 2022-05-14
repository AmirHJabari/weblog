namespace Weblog.Domain.Entities;

public class Tag : AuditableEntity, IHasDomainEvent
{
    public Tag()
    {
        BlogRelations = new List<BlogToTagRelation>();
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual IList<BlogToTagRelation> BlogRelations { get; set; }
    public List<DomainEvent> DomainEvents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}