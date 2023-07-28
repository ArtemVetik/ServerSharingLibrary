using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServerSharingAPI
{
    public static class Extentions
    {
        public static List<SelectResponceData> ParseSelectResponceData(this Response responce)
        {
            if (responce.IsSuccess == false)
                throw new InvalidOperationException($"Incorrect request\n" +
                                                    $"StatusCode: {responce.StatusCode}\n" +
                                                    $"Reason: {responce.ReasonPhrase}");

            return JsonConvert.DeserializeObject<List<SelectResponceData>>(responce.Body);
        }
    }
}