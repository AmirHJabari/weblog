namespace Weblog.Domain.Entities;

public class BaseManyToMany<TFromEntity, TToEntity> 
    where TFromEntity : class
    where TToEntity : class
{
    public int FromId { get; set; }
    public int ToId { get; set; }

    public virtual TFromEntity From { get; set; }
    public virtual TToEntity To { get; set; }
}