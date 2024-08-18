using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database.Models;

[Collection("routeDataPoint")]
public class RouteDataPoint : ModelBase
{
    [BsonElement("geometry")]
    public GeometryData? Geometry { get; set; }
    [BsonElement("properties")]
    public DataPointProperties? Properties { get; set; }

    [BsonElement("userProfileId")]
    public ObjectId UserProfileId { get; set; }

    [BsonElement("routeDataSetId")]
    public ObjectId RouteDataSetId { get; set; }

    public class GeometryData
    {
        [BsonElement("type")]
        public string Type { get; set; } = default!;
        [BsonElement("coordinates")]
        public int[] Coordinates { get; set; } = new int[0];
    }

    public class DataPointProperties
    {

        [BsonElement("speed")]
        public string? Speed { get; set; }
        [BsonElement("speedUnit")]
        public string? SpeedUnit { get; set; }
        [BsonElement("direction")]
        public string? Direction { get; set; }
    }
}