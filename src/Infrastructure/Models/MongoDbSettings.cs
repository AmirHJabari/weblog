namespace Weblog.Infrastructure.Models;

public class MongoDbSettings : IMongoDbSettings
{
    public string ConnectionSettings { get; set; }
    public string DatabaseName { get; set; }
    public string BlogsSerieCollectionName { get; set; }
}