using Weblog.Application.Common.Models;
using Weblog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Weblog.Application.Common.Interfaces;

namespace Weblog.Application.EventHandlers;

public class BlogUpdatedEventHandler : BaseEventHandler<BlogUpdatedEventHandler, BlogUpdatedEvent>
{
    private readonly IBlogsSerieRepository _blogsSerieRepository;

    public BlogUpdatedEventHandler(ILogger<BlogUpdatedEventHandler> logger, IBlogsSerieRepository blogsSerieRepository)
        : base(logger)
    {
        _blogsSerieRepository = blogsSerieRepository;
    }

    public override async Task HandleEvent(DomainEventNotification<BlogUpdatedEvent> notification, CancellationToken cancellationToken)
    {
        var blog = notification.DomainEvent.Item;
        var blogSeries = await _blogsSerieRepository.GetManyByBlogIdAsync(blog.Id, cancellationToken);
        foreach (var blogSerie in blogSeries)
        {
            var blogItem = blogSerie.Blogs.FirstOrDefault();

            blogItem.Title = blog.Title;
            blogItem.UrlTitle = blog.UrlTitle;
        }

        await _blogsSerieRepository.UpdateManyAsync(blogSeries, cancellationToken);
    }
}
