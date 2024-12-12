using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;

namespace MvLibs
{
    public class ExpandableObjectJsonConverter : JsonConverter
    {
        public override Boolean CanConvert(Type objectType) =>
            TypeDescriptor.GetAttributes(objectType).OfType<TypeConverterAttribute>()
                .Any(a => a.ConverterTypeName == typeof(ExpandableObjectConverter).FullName);

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            Object target = Activator.CreateInstance(objectType);
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(objectType))
            {
                JToken token = jo[prop.Name];
                if (token == null || token.Type == JTokenType.Null) continue;
                Object value = token.ToObject(prop.PropertyType, serializer);
                prop.SetValue(target, value);
            }
            return target;
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
            {
                if (!prop.IsBrowsable) continue;
                Object propValue = prop.GetValue(value);
                if (propValue == null) continue;
                jo.Add(prop.Name, JToken.FromObject(propValue, serializer));
            }
            jo.WriteTo(writer);
        }
    }

    //public class EnumStringConverter : JsonConverter
    //{
    //    public override Boolean CanConvert(Type objectType) => objectType.IsEnum;

    //    public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.String)
    //            return Enum.Parse(objectType, reader.Value.ToString());
    //        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enum.");
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
    //        writer.WriteValue(value.ToString());
    //}
}