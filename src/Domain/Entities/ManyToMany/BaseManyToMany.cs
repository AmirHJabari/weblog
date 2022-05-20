namespace Weblog.Domain.Entities;

public class BaseManyToMany<TFromEntity, TToEntity> 
    where TFromEntity : class
    where TToEntity : class
{
    public BaseManyToMany() { }
    public BaseManyToMany(TFromEntity from, int toId)
    {
        this.From = from;
        this.ToId = toId;
    }

    public BaseManyToMany(TToEntity to, int fromId)
    {
        this.FromId = fromId;
        this.To = to;
    }
    
    public BaseManyToMany(int fromId, int toId)
    {
        this.FromId = fromId;
        this.ToId = toId;
    }

    public int FromId { get; set; }
    public int ToId { get; set; }

    public virtual TFromEntity From { get; set; }
    public virtual TToEntity To { get; set; }
}