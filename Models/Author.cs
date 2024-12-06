using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace mongodb_net_api_crud.Models
{
    [BsonIgnoreExtraElements]
    public class Author
    {
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }
    }
}
