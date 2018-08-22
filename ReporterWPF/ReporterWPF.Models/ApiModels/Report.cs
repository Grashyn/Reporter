using Newtonsoft.Json;
using System;

namespace ReporterWPF.Models.ApiModels
{
    public class Report
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; internal set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; internal set; }

        [JsonProperty("CreatorId")]
        public Guid CreatorId { get; set; }

        [JsonProperty("AssigneeId")]
        public Guid AssigneeId { get; set; }
    }
}
