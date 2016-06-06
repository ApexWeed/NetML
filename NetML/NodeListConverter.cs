using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetML
{
    class NodeListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<Node>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (NodeStore.Nodes == null)
            {
                throw new NullReferenceException("Public node store was null.");
            }

            var nodes = new List<Node>();

            var array = JArray.Load(reader);
            foreach (var obj in array)
            {
                var name = (string)obj["Name"];
                var node = NodeStore.Nodes.First((x) => x.Name == name);
                nodes.Add(node);
            }

            return nodes;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var nodes = value as List<Node>;
            writer.WriteStartArray();
            foreach (var node in nodes)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Name");
                serializer.Serialize(writer, node.Name);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
