using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Domain.Events;
using MediatR;
using Weblog.Domain.Enums;

namespace Weblog.Application.Commands.CreateBlogsSerie;

public class CreateBlogsSerieCommand : IRequest<string>
{
    public string Title { get; set; }

    /// <summary>
    /// The http url friendly title.
    /// </summary>
    public string UrlTitle { get; set; }

    /// <summary>
    /// The blogs of this serie by order.
    /// </summary>
    public IList<int> BlogsId { get; set; }
}

public class CreateBlogsSerieCommandHandler : IRequestHandler<CreateBlogsSerieCommand, string>
{
    private readonly IBlogsSerieRepository _blogsSerieRepository;
    private readonly IApplicationDbContext _context;

    public CreateBlogsSerieCommandHandler(IBlogsSerieRepository blogsSerieRepository, IApplicationDbContext context)
    {
        _blogsSerieRepository = blogsSerieRepository;
        _context = context;
    }

    public async Task<string> Handle(CreateBlogsSerieCommand request, CancellationToken cancellationToken)
    {
        BlogsSerie blogsSerie = new()
        {
            Title = request.Title,
            UrlTitle = request.UrlTitle
        };

        foreach (var id in request.BlogsId)
        {
            var blog = await _context.Blogs.FindAsync(id);
            blogsSerie.Blogs.Add(new()
            {
                Id = blog.Id,
                Title = blog.Title,
                UrlTitle = blog.UrlTitle
            });
        }

        await _blogsSerieRepository.InsertAsync(blogsSerie, cancellationToken);

        return blogsSerie.Id;
    }
}