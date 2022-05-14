namespace Weblog.Domain.Entities;

public class Tag
{
    public Tag()
    {
        Children = new List<Tag>();
        BlogsRelation = new List<BlogToTagRelation>();
    }

    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }

    public virtual Tag Parent { get; set; }
    public virtual IList<Tag> Children { get; set; }

    public virtual IList<BlogToTagRelation> BlogsRelation { get; set; }
}