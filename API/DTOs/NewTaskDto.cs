using System;
using Newtonsoft.Json;

namespace API.DTOs
{
    public class NewTaskDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }
    }
}