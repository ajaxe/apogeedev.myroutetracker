using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("route_data_point")]
public class RouteDataPoint : ModelBase
{
    [BsonElement("geometry")]
    public GeometryData? Geometry { get; set; }
    [BsonElement("timestamp")]
    public DateTime? Timestamp { get; set; }
    [BsonElement("properties")]
    public DataPointProperties? Properties { get; set; }

    [BsonElement("user_profile_id")]
    public string UserProfileId { get; set; } = default!;

    [BsonElement("route_data_set_id")]
    public ObjectId RouteDataSetId { get; set; }

    public class GeometryData
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Point";
        [BsonElement("coordinates")]
        /// <summary>
        /// [longitude, latitude]
        /// </summary>
        /// <value></value>
        public decimal[] Coordinates { get; set; } = new decimal[0];
    }

    public class DataPointProperties
    {

        [BsonElement("speed")]
        public decimal? Speed { get; set; }
        [BsonElement("speed_unit")]
        public string? SpeedUnit { get; set; } = "m/sec";
        [BsonElement("heading")]
        public decimal? Heading { get; set; }
    }
}