

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyRbac.Web.WebExtentions
{
    public class LongToStringConverter : JsonConverter<long>
    {
        public LongToStringConverter()
        {
        }

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    JToken jt = JValue.ReadFrom(reader);

        //    return jt.Value<long>();
        //}

        public override bool CanConvert(Type objectType)
        {
            return typeof(System.Int64).Equals(objectType);
        }

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    serializer.Serialize(writer, value.ToString());
        //}

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return Convert.ToInt64(value);
            }
            return 0;
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
