using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Weblog.Domain.Common;
using Weblog.Domain.Entities;

namespace Weblog.Infrastructure.Persistence.Configurations.MongoDb;

public class BaseEntityConfiguration : IBaseBsonClassMap<BaseEntity<string>, BlogsSerieConfiguration>
{
    public void Configure(BsonClassMap<BaseEntity<string>> bsonClassMap)
    {
        bsonClassMap.MapIdMember(x => x.Id)
            .SetSerializer(new StringSerializer(BsonType.ObjectId))
            .SetIsRequired(true);

        bsonClassMap.SetIsRootClass(true);
    }
}

public class AuditableEntityConfiguration : IBaseBsonClassMap<AuditableEntity, BlogsSerieConfiguration>
{
    public void Configure(BsonClassMap<AuditableEntity> bsonClassMap)
    {
        bsonClassMap.MapMember(x => x.CreatedAt).SetIsRequired(true);
        bsonClassMap.MapMember(x => x.LastModified).SetIsRequired(true);

        bsonClassMap.SetIsRootClass(true);
    }
}
