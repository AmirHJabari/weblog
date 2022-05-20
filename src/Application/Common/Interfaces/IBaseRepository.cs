using Weblog.Domain.Common;

namespace Weblog.Application.Common.Interfaces;

public interface IBaseRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    where TEntity : BaseEntity<TKey>
{
    Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TKey> InsertAsync(TEntity obj, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity obj, CancellationToken cancellationToken = default);

    Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);
}