using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Weblog.Domain.Common;
using Weblog.Domain.Entities;

namespace Weblog.Infrastructure.Persistence.Configurations.MongoDb;

public class BlogsSerieConfiguration : IBaseBsonClassMap<BlogsSerie, BlogsSerieConfiguration>
{
    public void Configure(BsonClassMap<BlogsSerie> bsonClassMap)
    {
        bsonClassMap.GetMemberMap(x => x.Title).SetIsRequired(true);
        bsonClassMap.GetMemberMap(x => x.UrlTitle).SetIsRequired(true);
        bsonClassMap.GetMemberMap(x => x.Blogs).SetIsRequired(true);
    }
}

public class BlogsSerieItemConfiguration : IBaseBsonClassMap<BlogsSerieItem, BlogsSerieConfiguration>
{
    public void Configure(BsonClassMap<BlogsSerieItem> bsonClassMap)
    {
        bsonClassMap.GetMemberMap(x => x.Id).SetIsRequired(true);
        bsonClassMap.GetMemberMap(x => x.Title).SetIsRequired(true);
        bsonClassMap.GetMemberMap(x => x.UrlTitle).SetIsRequired(true);
    }
}
