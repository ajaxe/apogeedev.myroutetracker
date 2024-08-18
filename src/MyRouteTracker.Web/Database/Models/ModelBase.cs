using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyRouteTracker.Web.Database.Models;

public abstract class ModelBase
{
    public ObjectId Id { get; set; }
    [BsonElement("insertDate")]
    public DateTime? InsertDate { get; set; }
    [BsonElement("updateDate")]
    public DateTime? UpdateDate { get; set; }
}