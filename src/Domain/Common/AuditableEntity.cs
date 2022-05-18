namespace Weblog.Domain.Common;

public abstract class AuditableEntity
{
    public AuditableEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
}
