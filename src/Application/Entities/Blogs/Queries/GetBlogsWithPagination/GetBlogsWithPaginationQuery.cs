using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.GetBlogsWithPagination;

public class GetBlogsWithPaginationQuery : IRequest<PaginatedList<BlogBriefDto>>
{
    public bool ExcludeNonPublic { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetBlogsWithPaginationQueryHandler : IRequestHandler<GetBlogsWithPaginationQuery, PaginatedList<BlogBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BlogBriefDto>> Handle(GetBlogsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Blog> query = _context.Blogs;

        if (request.ExcludeNonPublic)
        {
            query = query.Where(x => x.Status == BlogStatus.Public);
        }
        
        return await query
            .ProjectTo<BlogBriefDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
