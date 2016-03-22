using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetML
{
    /// <summary>
    /// Converts a node to only store the name.
    /// </summary>
    public class NodeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Node);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (NodeStore.Nodes == null)
            {
                throw new NullReferenceException("Public node store was null.");
            }

            var o = JObject.Load(reader);
            var properties = o.Properties().ToList();
            var name = (string)properties[0].Value;
            var node = NodeStore.Nodes.First((x) => x.Name == name);

            if (node == null)
            {
                throw new NullReferenceException($"The node \"{name}\" was not found in the node store.");
            }

            return node;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = value as Node;
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            serializer.Serialize(writer, node.Name);
            writer.WriteEndObject();
        }
    }
}
