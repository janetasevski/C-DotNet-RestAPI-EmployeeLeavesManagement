using Newtonsoft.Json;

namespace EmployeeLeave.Core.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string First_name { get; set; }

        [JsonProperty("last_name")]
        public string Last_name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

    }
}