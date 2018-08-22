using Newtonsoft.Json;

namespace ReporterWPF.Models.ApiModels
{
    public class UserResponse
    {
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
