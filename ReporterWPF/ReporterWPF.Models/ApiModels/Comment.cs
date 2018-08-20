using Newtonsoft.Json;
using System;

namespace ReporterWPF.Models.ApiModels
{
    public class Comment
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("PostDate")]
        public DateTime PostDate { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("User")]
        public UserResponse User { get; set; }
    }
}
