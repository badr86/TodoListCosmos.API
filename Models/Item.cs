using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TodoListCosmos.API.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("isComplete")]
        public bool Completed { get; set; }
    }
}
