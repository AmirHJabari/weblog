using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken = default)
        => queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
}
