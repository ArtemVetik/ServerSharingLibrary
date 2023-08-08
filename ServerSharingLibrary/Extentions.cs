using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ServerSharing.Data;

namespace ServerSharingLibrary
{
    public static class Extentions
    {
        public static List<SelectResponseData> ParseSelectResponceData(this Response responce)
        {
            if (responce.IsSuccess == false)
                throw new InvalidOperationException($"Incorrect request\n" +
                                                    $"StatusCode: {responce.StatusCode}\n" +
                                                    $"Reason: {responce.ReasonPhrase}");

            return JsonConvert.DeserializeObject<List<SelectResponseData>>(responce.Body);
        }
    }
}