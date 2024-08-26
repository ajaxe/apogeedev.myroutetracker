using Newtonsoft.Json;

namespace MyRouteTracker.Web.Helpers.Newtonsoft;

public class MillisecondEpochConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader,
        Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (long.TryParse(reader.Value as string, out long ticks))
        {
            return DateTime.UnixEpoch.AddMilliseconds(ticks);
        }
        return existingValue;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(((DateTime)value! - DateTime.UnixEpoch).TotalMilliseconds);
    }
}
