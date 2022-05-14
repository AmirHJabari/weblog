namespace Weblog.Domain.Entities;

public class BaseManyToMany<TFromEntity, TToEntity>
{
    public int FromEntityId { get; set; }
    public int ToEntityId { get; set; }

    public virtual TFromEntity FromEntity { get; set; }
    public virtual TToEntity ToEntity { get; set; }
}