using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("routeDataSet")]
public class RouteDataSet : ModelBase
{
    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("mode")]
    public string? Mode { get; set; }

    [BsonElement("user_profile_id")]
    public string UserProfileId { get; set; } = default!;

    [BsonElement("mark_for_deletion")]
    public bool? MarkForDeletion { get; set; }
}