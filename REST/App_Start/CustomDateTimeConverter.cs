using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace REST.App_Start
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return DateTime.ParseExact(reader.Value.ToString(), "yyyy'-'MM'-'dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                //return DateTime.Parse(reader.Value.ToString());
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((DateTime)value).ToString("yyyy'-'MM'-'dd'T'HH:mm:ss"));
                return;
            }
            writer.WriteNull();
        }
    }
}