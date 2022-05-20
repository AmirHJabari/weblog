namespace Weblog.Domain.Entities;

public class BlogToTagRelation : BaseManyToMany<Blog, Tag>
{
    public BlogToTagRelation() : base() { }
    public BlogToTagRelation(Blog blog, int tagId) : base(blog, tagId) { }
    public BlogToTagRelation(Tag tag, int blogId) : base(tag, blogId) { }
    public BlogToTagRelation(int blogId, int tagId) : base(blogId, tagId) { }
}