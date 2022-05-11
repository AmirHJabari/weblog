using Weblog.Domain.Common;

namespace Weblog.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
