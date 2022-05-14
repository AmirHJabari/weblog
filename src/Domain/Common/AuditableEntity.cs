namespace Weblog.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
}
