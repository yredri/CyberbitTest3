using System;
using Newtonsoft.Json;

namespace API.DTOs
{
    public class TaskDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("completed")]
        public bool Completed { get; set; }
        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}