using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImperialBreath.Website.Models
{
    public class Subject
    {
        [JsonPropertyName("subjectId")]
        public int SubjectId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
