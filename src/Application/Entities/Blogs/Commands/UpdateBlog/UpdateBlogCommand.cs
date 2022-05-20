using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Domain.Events;
using MediatR;
using Weblog.Domain.Enums;
using Weblog.Application.Common.Exceptions;

namespace Weblog.Application.Commands.UpdateBlog;

public class UpdateBlogCommand : IRequest
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    
    public string Content { get; set; }

    public string PosterImage { get; set; }

    public string Summary { get; set; }

    public BlogStatus Status { get; set; }

    public string UrlTitle { get; set; }

    public string Title { get; set; }

    public List<int> TagsIds { get; set; }
}

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Blogs
            .FindAsync(new[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.Title = request.Title;
        entity.CategoryId = request.CategoryId;
        entity.Content = request.Content;
        entity.PosterImage = request.PosterImage;
        entity.Summary = request.Summary;
        entity.Status = request.Status;
        entity.UrlTitle = request.UrlTitle;

        // TODO: check if this works
        entity.TagRelations = request.TagsIds.Select(tagId => new BlogToTagRelation(entity.Id, tagId)).ToList();
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
