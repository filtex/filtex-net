using System.Text.Json.Serialization;

namespace FiltexNet.Models
{
    public class Field
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("operators")]
        public string[] Operators { get; set; }

        [JsonPropertyName("values")]
        public Lookup[] Values { get; set; }
    }
}