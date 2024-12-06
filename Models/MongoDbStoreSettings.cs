namespace mongodb_net_api_crud.Models
{
    public class MongoDbStoreSettings : IMongoDbStoreSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }
}
