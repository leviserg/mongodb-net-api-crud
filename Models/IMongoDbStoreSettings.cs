namespace mongodb_net_api_crud.Models
{
    public interface IMongoDbStoreSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
