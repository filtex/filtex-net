using System.Text.Json.Serialization;

namespace FiltexNet.Models
{
    public class Lookup
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("value")]
        public object Value { get; }

        public Lookup(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}