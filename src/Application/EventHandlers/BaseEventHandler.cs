using Weblog.Application.Common.Models;
using Weblog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Weblog.Domain.Common;

namespace Weblog.Application.EventHandlers;

public abstract class BaseEventHandler<THandler, TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
            where TDomainEvent : DomainEvent
            where THandler : BaseEventHandler<THandler, TDomainEvent>
{
    protected readonly ILogger<THandler> Logger;

    public BaseEventHandler(ILogger<THandler> logger)
    {
        Logger = logger;
    }

    public Task Handle(DomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        Logger.LogInformation("Weblog Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return HandleEvent(notification, cancellationToken);
    }

    public abstract Task HandleEvent(DomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken);
}
