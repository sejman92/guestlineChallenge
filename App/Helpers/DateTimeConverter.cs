using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuestlineChallenge.Helpers;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), Consts.DateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        throw new JsonException($"Date format should be {Consts.DateFormat}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Consts.DateFormat));
    }
}