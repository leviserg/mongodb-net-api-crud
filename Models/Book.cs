using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb_net_api_crud.Models
{

    [BsonIgnoreExtraElements]
    public class Book
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)] - when using default mongodb "_id" as objectId

        [BsonElement("_id")]
        public int? Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; } = string.Empty;

        [BsonElement("issued")]
        public DateOnly? Issued { get; set; }

        [BsonElement("authors")]
        public IEnumerable<Author>? Authors { get; set; } = Enumerable.Empty<Author>();

    }
}
