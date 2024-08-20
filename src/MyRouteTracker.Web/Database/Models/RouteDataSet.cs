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
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserProfileId { get; set; }
    [BsonElement("markForDeletion")]
    public bool? MarkForDeletion { get; set; }
}