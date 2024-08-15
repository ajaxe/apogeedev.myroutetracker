using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("userProfiles")]
public class UserProfile : ModelBase
{
    public ObjectId Id { get; set; }
    [BsonElement("authenticationType")]
    public string AuthenticationType { get; set; }
    [BsonElement("externalId")]
    public string ExternalId { get; set; }
}