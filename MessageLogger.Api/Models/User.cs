using System.Text.Json.Serialization;

namespace MessageLogger.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
    }
}
