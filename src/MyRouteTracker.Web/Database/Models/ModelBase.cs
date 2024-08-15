using MongoDB.Bson.Serialization.Attributes;

namespace MyRouteTracker.Web.Database.Models;

public abstract class ModelBase
{
    [BsonElement("insertDate")]
    public DateTime? InsertDate { get; set; }
    [BsonElement("updateDate")]
    public DateTime? UpdateDate { get; set; }
}