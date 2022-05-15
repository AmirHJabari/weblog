using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;

namespace Weblog.Application.Queries.GetBlogsWithPagination;

public class GetBlogsWithPaginationQuery : IRequest<PaginatedList<BlogBriefDto>>
{
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
        return await _context.Blogs
            .ProjectTo<BlogBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
