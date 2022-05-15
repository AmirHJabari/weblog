using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;

namespace Weblog.Application.Queries.GetBlogsOfCategoryWithPagination;

public class GetBlogsOfCategoryWithPaginationQuery : IRequest<PaginatedList<BlogBriefDto>>
{
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
        return await _context.Blogs
            .Where(x => x.CategoryId == request.CategoryId)
            .ProjectTo<BlogBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
