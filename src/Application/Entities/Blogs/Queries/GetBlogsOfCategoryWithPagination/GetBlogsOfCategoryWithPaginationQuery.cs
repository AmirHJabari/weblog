using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;

namespace Weblog.Application.Queries.GetBlogsOfCategoryWithPagination;

public class GetBlogsOfCategoryWithPaginationQuery : IRequest<PaginatedList<BlogBriefDto>>
{
    public bool ExcludeNonPublic { get; set; } = true;
    public int CategoryId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetBlogsOfCategoryWithPaginationQueryHandler : IRequestHandler<GetBlogsOfCategoryWithPaginationQuery, PaginatedList<BlogBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBlogsOfCategoryWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BlogBriefDto>> Handle(GetBlogsOfCategoryWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Blog> query = _context.Blogs;

        if (request.ExcludeNonPublic)
        {
            query = query.Where(x => x.CategoryId == request.CategoryId && x.Status == BlogStatus.Public);
        }
        else
        {
            query = query.Where(x => x.CategoryId == request.CategoryId);
        }

        return await query
            .ProjectTo<BlogBriefDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
