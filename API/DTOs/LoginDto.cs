using Newtonsoft.Json;

namespace API.DTOs
{
    public class LoginDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}