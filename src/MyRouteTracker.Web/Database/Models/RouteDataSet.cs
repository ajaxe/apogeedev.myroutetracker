using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("routeDataSet")]
public class RouteDataSet : ModelBase
{
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("mode")]
    public string? Mode { get; set; }
    [BsonElement("userProfileId")]
    public ObjectId UserProfileId { get; set; }
}