using System;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Extensions
{
    public static class ControllerExtentsions
    {
        public static OkObjectResult Ok(this ControllerBase c, object value)
        {
            var res = c.Ok(value);
            /*res.Formatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.J(
                new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented },
                System.Buffers.ArrayPool<char>.Create()));*/
            return res;
        }
    }

    public class WeirdNameSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var xml = JsonConvert.SerializeObject(value);
            writer.WriteValue(xml);
            //serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = serializer.Deserialize(reader, objectType);
            return json;
        }

        public override bool CanConvert(Type objectType) => true;
    }
}