using Weblog.Application.Common.Models;
using Weblog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Weblog.Application.EventHandlers;

public class TodoItemCompletedEventHandler : BaseEventHandler<TodoItemCreatedEventHandler, TodoItemCreatedEvent>
{
    public TodoItemCompletedEventHandler(ILogger<TodoItemCreatedEventHandler> logger) 
        : base(logger)
    {
        
    }

    public override Task HandleEvent(DomainEventNotification<TodoItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
