using System;
using Newtonsoft.Json;

namespace ServerSharingAPI
{
    public class SelectResponceData
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("record")] public string Record { get; set; }
        [JsonProperty("datetime")] public DateTime Datetime { get; set; }
        [JsonProperty("downloads")] public ulong Downloads { get; set; }
        [JsonProperty("likes")] public ulong Likes { get; set; }
    }
}