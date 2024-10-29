using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuestlineChallenge.Helpers;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _format = "yyyyMMdd";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        throw new JsonException($"Date format should be {_format}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}