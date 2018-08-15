using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ReporterWPF.Models.ApiModels
{
    public class ReportExtended
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("Creator")]
        public UserResponse Creator { get; set; }

        [JsonProperty("Assignee")]
        public UserResponse Assignee { get; set; }

        [JsonProperty("Comments")]
        public List<Comment> Comments { get; set; }
    }
}
