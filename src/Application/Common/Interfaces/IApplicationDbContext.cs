using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Weblog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Blog> Blogs { get; }

    DbSet<Tag> Tags { get; }

    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
