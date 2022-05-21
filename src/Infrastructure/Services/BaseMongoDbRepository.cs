using MongoDB.Driver;
using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Common;

namespace Weblog.Infrastructure.Services;

public abstract class BaseMongoDbRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    where TEntity : BaseEntity<TKey>
{
    protected abstract IMongoCollection<TEntity> Collection { get; }

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var cursor = await Collection.FindAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public Task InsertAsync(TEntity obj, CancellationToken cancellationToken = default)
    {
        return Collection.InsertOneAsync(obj, new InsertOneOptions(), cancellationToken);
    }

    public Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return Collection.DeleteOneAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public Task UpdateAsync(TEntity obj, CancellationToken cancellationToken = default)
    {
        return Collection.ReplaceOneAsync(x => x.Id.Equals(obj.Id), obj, cancellationToken: cancellationToken);
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> objs, CancellationToken cancellationToken = default)
    {
        var updates = new List<WriteModel<TEntity>>();
        var filterBuilder = Builders<TEntity>.Filter;

        foreach (var obj in objs)
        {
            var filter = filterBuilder.Where(x => x.Id.Equals(obj.Id));
            updates.Add(new ReplaceOneModel<TEntity>(filter, obj));
        }

        await Collection.BulkWriteAsync(updates);
    }
}