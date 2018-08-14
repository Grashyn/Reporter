using Newtonsoft.Json;

namespace ReporterWPF.Models
{
    public class LoginData
    {
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
