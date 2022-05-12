namespace Weblog.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }

    public virtual Tag Parent { get; set; }
    public virtual ICollection<Tag> Children { get; set; }

    public virtual ICollection<BlogToTagRelation> BlogsRelation { get; set; }
}