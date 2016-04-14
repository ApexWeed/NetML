using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetML
{
    class IDrawableConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Node) || objectType == typeof(Link) || objectType == typeof(Stream))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = JObject.Load(reader);
            var properties = o.Properties().ToList();
            var type = (string)properties[0].Value;
            var name = (string)properties[1].Value;

            switch (type)
            {
                case nameof(Node):
                    {
                        return new Node { Name = name };
                    }
                case nameof(Link):
                    {
                        return new Link { Name = name };
                    }
                case nameof(Stream):
                    {
                        return new Stream { Name = name };
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Node)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                serializer.Serialize(writer, nameof(Node));
                writer.WritePropertyName("Name");
                serializer.Serialize(writer, (value as Node).Name);
                writer.WriteEndObject();
            }
            else if (value is Link)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                serializer.Serialize(writer, nameof(Link));
                writer.WritePropertyName("Name");
                serializer.Serialize(writer, (value as Link).Name);
                writer.WriteEndObject();
            }
            else if (value is Stream)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                serializer.Serialize(writer, nameof(Stream));
                writer.WritePropertyName("Name");
                serializer.Serialize(writer, (value as Stream).Name);
                writer.WriteEndObject();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
