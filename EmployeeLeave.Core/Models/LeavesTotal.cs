using Newtonsoft.Json;

namespace EmployeeLeave.Core.Models
{
    public class LeavesTotal
    {
        [JsonProperty("employee_id")]
        public int Employee_id { get; set; }

        [JsonProperty("first_name")]
        public string First_name { get; set; }

        [JsonProperty("last_name")]
        public string Last_name { get; set; }

        [JsonProperty("spent_days")]
        public int Spent_days { get; set; }

        [JsonProperty("total_days")]
        public int Total_days { get; set; }

        [JsonProperty("left_days")]
        public int Left_days { get; set; }
    }
}
