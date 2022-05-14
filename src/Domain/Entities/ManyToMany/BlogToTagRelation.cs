namespace Weblog.Domain.Entities;

public class BlogToTagRelation : BaseManyToMany<Blog, Tag>
{ 
    public BlogToTagRelation() { }
    public BlogToTagRelation(Blog blog, int tagId)
    {
        this.From = blog;
        this.ToId = tagId;
    }

    public List<Blog> Blogs { get; set; }
}