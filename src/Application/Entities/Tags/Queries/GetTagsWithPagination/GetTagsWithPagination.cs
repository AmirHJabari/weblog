using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Application.Queries.GetTagsWithPagination;

public class GetTagsWithPaginationQuery : IRequest<PaginatedList<TagDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

internal class GetTagsWithPaginationQueryHandler : IRequestHandler<GetTagsWithPaginationQuery, PaginatedList<TagDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TagDto>> Handle(GetTagsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tags;

        // var categories = _context.Categories.Select(x => new CategoryDto()
        // {
        //     Id = x.Id,
        //     Name = x.Name,
        //     UrlName = x.UrlName,
        //     BlogsCount = x.Blogs.Count()
        // });
        // var a = PaginatedList<CategoryDto>.CreateAsync(categories, request.PageNumber, request.PageSize);

        return await query
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
