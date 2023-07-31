using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ServerSharingLibrary
{
    public static class ServerSharing
    {
        private static readonly HttpClient _client = new HttpClient();

        private static string _function;
        private static string _userId;

        public static void Initialize(string function, string userId)
        {
            if (Initialized)
                throw new InvalidOperationException(nameof(ServerSharing) + " has already been initialized");

            _function = function ?? throw new ArgumentNullException(nameof(function));
            _userId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public static bool Initialized => _function != null && _userId != null;

        public async static Task<Response> Upload(string record)
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharing) + " is not initialized");

            var request = new Request("UPLOAD", _userId, record);
            return await Post(request);
        }

        public async static Task<Response> Download(string id)
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharing) + " is not initialized");

            var request = new Request("DOWNLOAD", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Delete(string id)
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharing) + " is not initialized");

            var request = new Request("DELETE", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Like(string id)
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharing) + " is not initialized");

            var request = new Request("LIKE", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Select(EntryType entryType, Sorting sort, Order order, ulong limit = 10, ulong offset = 0)
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharing) + " is not initialized");

#pragma warning disable IDE0037 // Use inferred member name
            var body = new
            {
                entry_type = entryType,
                sort = sort.ToString(),
                order = order.ToString(),
                limit = limit,
                offset = offset,
            };
#pragma warning restore IDE0037 // Use inferred member name

            var request = new Request("SELECT", _userId, JsonConvert.SerializeObject(body));
            var response = await Post(request);

            return response;
        }

        private async static Task<Response> Post(Request request)
        {
            var body = new StringContent(JsonConvert.SerializeObject(request));
            var response = await _client.PostAsync($"https://functions.yandexcloud.net/{_function}?integration=raw", body);

            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Response>(responseString);
        }
    }
}