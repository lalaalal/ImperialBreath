using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImperialBreath.Website.Models
{
    public class Group
    {
        [JsonPropertyName("subjects")]
        public Subject[] Subjects { get; set; }

        [JsonPropertyName("tasks")]
        public Models.Task[] Tasks { get; set; }
    }
}
