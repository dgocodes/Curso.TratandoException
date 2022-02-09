using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace School.API.Extensions
{
    public static class Utf8JsonReaderExtensions
    {
        public static bool TryGetString(this Utf8JsonReader reader, out string value)
        {
            value = string.Empty;
            try
            {
                value = reader.GetString();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }

    public class DateOnlyConverterNullable : JsonConverter<DateOnly?>
    {
        private const string DateFormat = "yyyy-MM-dd";
        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!reader.TryGetString(out string value))            
                return null;            

            return DateOnly.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value == null ? string.Empty : value.Value.ToString(DateFormat, CultureInfo.InvariantCulture));

    }

    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                return DateOnly.FromDateTime(DateTime.Now);

            return DateOnly.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture) ?? string.Empty);
    }

    public static class DateOnlyConverterExtensions
    {
        public static JsonSerializerOptions AddDateOnlyConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new DateOnlyConverter());
            options.Converters.Add(new DateOnlyConverterNullable());

            return options;
        }
    }
}
