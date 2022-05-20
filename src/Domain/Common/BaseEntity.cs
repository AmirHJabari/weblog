namespace Weblog.Domain.Common;

public abstract class BaseEntity<TKey> : AuditableEntity
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
{
    public BaseEntity() : base()
    {  }

    public virtual TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{  }