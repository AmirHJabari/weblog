namespace Weblog.Domain.Entities;

public class Category
{
    public Category()
    {
        Blogs = new List<Blog>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual IList<Blog> Blogs { get; set; }
}