using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MyRouteTracker.Web.Database.Models;

public abstract class ModelBase
{
    public ModelBase()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("insertDate")]
    public DateTime? InsertDate { get; set; }
    [BsonElement("updateDate")]
    public DateTime? UpdateDate { get; set; }
}