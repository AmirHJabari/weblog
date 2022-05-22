using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Mappings;
using Weblog.Application.Common.Models;
using MediatR;
using Weblog.Domain.Entities;
using Weblog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Application.Queries.GetCategoriesWithPagination;

public class GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

internal class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories;

        // var categories = _context.Categories.Select(x => new CategoryDto()
        // {
        //     Id = x.Id,
        //     Name = x.Name,
        //     UrlName = x.UrlName,
        //     BlogsCount = x.Blogs.Count()
        // });
        // var a = PaginatedList<CategoryDto>.CreateAsync(categories, request.PageNumber, request.PageSize);

        return await query
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
