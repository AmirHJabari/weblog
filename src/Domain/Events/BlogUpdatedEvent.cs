namespace Weblog.Domain.Events;

public class BlogUpdatedEvent : DomainEvent
{
    public BlogUpdatedEvent(Blog item)
    {
        Item = item;
    }

    public Blog Item { get; }
}