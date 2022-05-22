using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Application.Queries.GetBlog;

public class GetBlogQuery : IRequest<BlogDto>
{
    public int Id { get; set; }
}

internal class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, BlogDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogDto> Handle(GetBlogQuery request, CancellationToken cancellationToken)
    {
        var blog = await _context.Blogs.Include(x => x.Category).Include(x => x.TagRelations).ThenInclude(x => x.To)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var blogDto = _mapper.Map<BlogDto>(blog);

        blogDto.Tags = _mapper.Map<TagDto[]>(blog.TagRelations.Select(x => x.To));
        
        return blogDto;
    }
}
