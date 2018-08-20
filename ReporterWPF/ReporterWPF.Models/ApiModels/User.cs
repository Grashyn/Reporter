using Newtonsoft.Json;
using System;

namespace ReporterWPF.Models.ApiModels
{
    public class User
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Rights")]
        public byte Rights { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
