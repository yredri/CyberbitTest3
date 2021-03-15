using Newtonsoft.Json;

namespace API.DTOs
{
    public class UserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        
    }
}