namespace Weblog.Infrastructure.Models;

public interface IMongoDbSettings : IIsSettingModel
{
    public string ConnectionSettings { get; set; }
    public string DatabaseName { get; set; }
    public string BlogsSerieCollectionName { get; set; }
}