using ServerSharing.Data;

namespace ServerSharingLibrary
{
    public class ExtendedResponse<T>
    {
        public readonly Response Response;
        public readonly T ConvertedData;

        public ExtendedResponse(Response response, T convertedData)
        {
            Response = response;
            ConvertedData = convertedData;
        }
    }
}