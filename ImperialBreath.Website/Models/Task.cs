using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImperialBreath.Website.Models
{
    public class Task
    {
        [JsonPropertyName("id")]
        public int TaskId { get; set; }
        [JsonPropertyName("subjectId")]
        public int SubjectId { get; set; }
        [JsonPropertyName("summary")]
        public string Summary { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("deadline")]
        public DateTime Deadline { get; set; }

        [JsonPropertyName("writtenDate")]
        public DateTime WrittenDate { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
