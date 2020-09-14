using Newtonsoft.Json;
using System;

namespace EmployeeLeave.Core.Models
{
    public class Leaves
    {
        [JsonProperty("employee_id")]
        public int Employee_id { get; set; }

        [JsonProperty("days")]
        public int Days { get; set; }

        [JsonProperty("start_date")]
        public DateTime Start_date { get; set; }

        [JsonProperty("end_date")]
        public DateTime End_date { get; set; }
    }
}
