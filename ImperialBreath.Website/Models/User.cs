using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImperialBreath.Website.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("pw")]
        public string Pw { get; set; }
        [JsonPropertyName("doneTaskId")]
        public int[] DoneTaskId { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
