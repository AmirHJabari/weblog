using MongoDB.Bson.Serialization;

namespace Weblog.Infrastructure.Persistence.Configurations.MongoDb;

public interface IBaseBsonClassMap<TClass, TChild>
    where TClass : class
    where TChild : class, new()
{
    void Configure(BsonClassMap<TClass> bsonClassMap);
}