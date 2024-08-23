using System.Text.Json;
using System.Text.Json.Serialization;

public class MicrosecondEpochConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var token = reader.GetInt64();
        return DateTime.UnixEpoch.AddMilliseconds(token);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(new DateTimeOffset(value).ToUnixTimeMilliseconds().ToString());
    }
}