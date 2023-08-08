using Newtonsoft.Json;
using ServerSharing.Data;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerSharingLibrary
{
    public static class ServerSharingApi
    {
        private static readonly HttpClient _client = new HttpClient();

        private static string _function;
        private static string _userId;

        public static void Initialize(string function, string userId)
        {
            if (Initialized)
                throw new InvalidOperationException(nameof(ServerSharingApi) + " has already been initialized");

            _function = function ?? throw new ArgumentNullException(nameof(function));
            _userId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public static bool Initialized => _function != null && _userId != null;

        public async static Task<Response> Upload(UploadData uploadData)
        {
            EnsureInitialize();

            var request = Request.Create("UPLOAD", _userId, JsonConvert.SerializeObject(uploadData));
            return await Post(request);
        }

        public async static Task<Response> Download(string id)
        {
            EnsureInitialize();

            var request = Request.Create("DOWNLOAD", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> LoadImage(string id)
        {
            EnsureInitialize();

            var request = Request.Create("LOAD_IMAGE", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Delete(string id)
        {
            EnsureInitialize();

            var request = Request.Create("DELETE", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Like(string id)
        {
            EnsureInitialize();

            var request = Request.Create("LIKE", _userId, id);
            return await Post(request);
        }

        public async static Task<Response> Select(EntryType entryType, SelectRequestBody.SelectOrderBy[] orderBy, ulong limit = 10, ulong offset = 0)
        {
            EnsureInitialize();

            var body = new SelectRequestBody()
            {
                EntryType = entryType,
                OrderBy = orderBy,
                Limit = limit,
                Offset = offset,
            };

            var request = Request.Create("SELECT", _userId, JsonConvert.SerializeObject(body));
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

        private static void EnsureInitialize()
        {
            if (Initialized == false)
                throw new InvalidOperationException(nameof(ServerSharingApi) + " is not initialized");
        }
    }
}