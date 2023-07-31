using Newtonsoft.Json;

namespace ServerSharingLibrary
{
    internal class Request
    {
        public Request(string method, string user_id, string body)
        {
            Method = method;
            UserId = user_id;
            Body = body;
        }

        [JsonProperty("method")] public string Method { get; private set; }
        [JsonProperty("user_id")] public string UserId { get; private set; }
        [JsonProperty("body")] public string Body { get; private set; }
    }
}