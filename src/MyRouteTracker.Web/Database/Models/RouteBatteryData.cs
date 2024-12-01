using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("routeBatteryData")]
public class RouteBatteryData : ModelBase
{
    [BsonElement("route_data_set_id")]
    public ObjectId RouteDataSetId { get; set; }
    [BsonElement("battery_level")]
    public decimal? BatteryLevel { get; set; }
}