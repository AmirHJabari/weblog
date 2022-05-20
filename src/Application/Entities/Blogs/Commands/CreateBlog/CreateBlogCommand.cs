using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Domain.Events;
using MediatR;
using Weblog.Domain.Enums;

namespace Weblog.Application.Commands.CreateBlog;

public class CreateBlogCommand : IRequest<int>
{
    public int CategoryId { get; set; }
    
    public string Content { get; set; }

    public string PosterImage { get; set; }

    public string Summary { get; set; }

    public BlogStatus Status { get; set; }

    public string UrlTitle { get; set; }

    public string Title { get; set; }

    public List<int> TagsIds { get; set; }
}

internal class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = new Blog
        {
            Title = request.Title,
            CategoryId = request.CategoryId,
            Content = request.Content,
            PosterImage = request.PosterImage,
            Summary = request.Summary,
            Status = request.Status,
            UrlTitle = request.UrlTitle
        };

        // TODO: check if this works
        entity.TagRelations = request.TagsIds.Select(tagId => new BlogToTagRelation(entity, tagId)).ToList();

        //entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

        _context.Blogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
