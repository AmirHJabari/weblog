using MongoDB.Driver;
using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Infrastructure.Models;

namespace Weblog.Infrastructure.Services;

public class BlogsSerieRepository : BaseMongoDbRepository<BlogsSerie, string>, IBlogsSerieRepository
{
    private readonly IDateTime _dateTime;
    private readonly IMongoDbSettings _mongoDbSettings;

    public BlogsSerieRepository(IDateTime dateTime, IMongoDbSettings mongoDbSettings)
    {
        _mongoDbSettings = mongoDbSettings;
        _dateTime = dateTime;

        var client = new MongoClient(_mongoDbSettings.ConnectionSettings);
        var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
        _collection = database.GetCollection<BlogsSerie>(_mongoDbSettings.BlogsSerieCollectionName);
    }

    readonly IMongoCollection<BlogsSerie> _collection;
    protected override IMongoCollection<BlogsSerie> Collection => _collection;

    public async Task<List<BlogsSerie>> GetManyByBlogIdAsync(int blogId, CancellationToken cancellationToken = default)
    {
        var cursor = await Collection.FindAsync(x => x.Blogs.Any(i => i.Id == blogId), cancellationToken: cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}